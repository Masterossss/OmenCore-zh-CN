from pathlib import Path
import csv, hashlib, html, re

ROOT = Path(__file__).resolve().parent.parent
APP = ROOT / 'src' / 'OmenCoreApp'
OUT = ROOT / 'LOCALIZATION-ZH-CN-AUDIT-SNAPSHOT.csv'
ATTR = re.compile(r'(?P<name>AutomationProperties\.(?:Name|HelpText)|Text|Content|Header|ToolTip|Title|StringFormat)\s*=\s*"(?P<value>[^"]*)"')
TEXT = re.compile(r'>(?P<value>[^<>\r\n]*[A-Za-z][^<>\r\n]*)<')
CS_LITERAL = re.compile(r'(?<![A-Za-z0-9_])(?:\$?@?"(?:""|\\.|[^"\\])*")')
DYNAMIC = re.compile(r'MessageBox\.Show\s*\(|\b(?:ShowNotification|ShowToast|ShowError|ShowWarning|ShowDialog)\s*\(|\b\w*(?:Status|StatusMessage|ErrorMessage|ToolTip)\s*=')
TECH = {'CPU','GPU','RAM','RGB','WMI','BIOS','EC','MSR','TGP','TDP','MUX','RPM','MB','GB','MHz','ms','s','OmenCore','GitHub'}

def main():
    counts = {}
    rows = []
    files = sorted(p for p in APP.rglob('*') if p.is_file() and p.suffix in {'.xaml', '.cs'} and not any(x in p.parts for x in ('obj','bin','runtimes')))
    def add(path, line, kind, raw):
        val = html.unescape(raw.strip())
        if not val or not re.search('[A-Za-z]', val): return
        key = (path,kind,val)
        counts[key] = counts.get(key,0)+1
        digest = hashlib.sha256(('\0'.join(key)+'\0'+str(counts[key])).encode('utf-8')).hexdigest()[:20]
        category,reason = ('E','待结合上下文和调用链核实')
        if val.startswith('{x:Static ') and 'Strings.' in val: category,reason = ('D','已有 Strings 资源引用；译文和显示仍需验证')
        elif kind != 'cs_literal' and val in TECH: category,reason = ('B','通用技术名称或单位')
        elif kind != 'cs_literal' and val.startswith(('{Binding ', '{StaticResource ', '{DynamicResource ', '{TemplateBinding ')): category,reason = ('C','绑定或资源标识；绑定值仍需追踪')
        rows.append((digest,path,line,kind,val,category,reason))
    for file in files:
        rel=file.relative_to(ROOT).as_posix()
        text=file.read_text(encoding='utf-8-sig',errors='replace')
        for number,line in enumerate(text.splitlines(),1):
            if file.suffix=='.xaml':
                for match in ATTR.finditer(line):add(rel,number,match.group('name'),match.group('value'))
                for match in TEXT.finditer(line):add(rel,number,'text_node',match.group('value'))
            elif not line.lstrip().startswith(('//','*')):
                if DYNAMIC.search(line):add(rel,number,'dynamic_entry',line.strip())
                for match in CS_LITERAL.finditer(line):add(rel,number,'cs_literal',match.group())
    with OUT.open('w',encoding='utf-8-sig',newline='') as f:
        w=csv.writer(f);w.writerow(('stable_id','path','line_at_scan','source','original','category','reason'));w.writerows(rows)
    print(f'{len(files)} files, {len(rows)} candidates written to {OUT.name}')
if __name__ == '__main__': main()
