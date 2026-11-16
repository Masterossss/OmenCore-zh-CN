"""Namespace-aware XAML inventory. Candidates require runtime/call-chain review."""
from pathlib import Path
from xml.parsers import expat
import csv, hashlib, re

ROOT = Path(__file__).resolve().parent.parent
APP = ROOT / 'src' / 'OmenCoreApp'
OUTPUT = ROOT / 'LOCALIZATION-ZH-CN-XAML-STRUCTURED.csv'
ATTRS = {'Text','Content','Header','ToolTip','Title','AutomationProperties.Name','AutomationProperties.HelpText','StringFormat'}
TECH = {'CPU','GPU','RAM','RGB','WMI','BIOS','EC','MSR','TGP','TDP','MUX','RPM','MB','GB','MHz','ms','s','OmenCore','GitHub'}
DESIGN = 'http://schemas.microsoft.com/expression/blend/2008'

def local(name): return name.rsplit('|', 1)[-1]
def category(name, value):
    if name.startswith(DESIGN + '|'): return 'C','设计时属性，运行时仍需确认'
    if value in TECH: return 'B','常用技术名称或单位'
    if value.startswith('{x:Static ') and 'Strings.' in value: return 'D','已有资源引用；实际绑定和译文待验证'
    if value.startswith(('{Binding ', '{StaticResource ', '{DynamicResource ', '{TemplateBinding ')): return 'C','绑定或资源引用；显示值需另行追踪'
    return 'E','静态值或文本节点，用户可见性及译文待核实'

def scan(file):
    relative=file.relative_to(ROOT).as_posix(); parser=expat.ParserCreate(namespace_separator='|'); stack=[]; rows=[]
    def emit(path,name,value,line,kind):
        value=value.strip()
        if not value or not re.search('[A-Za-z]',value): return
        cat,reason=category(name,value)
        fingerprint=hashlib.sha256('\0'.join((relative,path,name,value)).encode('utf-8')).hexdigest()[:20]
        rows.append([fingerprint,relative,line,path,kind,name,value,cat,reason])
    def start(name,attrs):
        if stack:
            siblings=stack[-1]['counts']; siblings[name]=siblings.get(name,0)+1; index=siblings[name]; parent=stack[-1]['path']
        else:index=1;parent=''
        path=parent+'/'+local(name)+'['+str(index)+']'
        frame={'path':path,'counts':{},'text':[],'line':parser.CurrentLineNumber};stack.append(frame)
        for key,value in attrs.items():
            if local(key) in ATTRS:
                emit(path,key,value,parser.CurrentLineNumber,'attribute')
                m=re.search(r'\bStringFormat\s*=\s*([^}]+)',value)
                if m:emit(path,key+':StringFormat',m.group(1),parser.CurrentLineNumber,'markup_stringformat')
    def text(value):
        if stack:stack[-1]['text'].append(value)
    def end(name):
        frame=stack.pop();emit(frame['path'],'text', ''.join(frame['text']),frame['line'],'text_node')
    parser.StartElementHandler=start;parser.EndElementHandler=end;parser.CharacterDataHandler=text
    parser.Parse(file.read_bytes(), True)
    return rows

def main():
    files=sorted(list((APP/'Views').glob('*.xaml'))+list((APP/'Controls').glob('*.xaml')))
    rows=[]
    for file in files: rows.extend(scan(file))
    with OUTPUT.open('w',encoding='utf-8-sig',newline='') as f:
        w=csv.writer(f);w.writerow(['stable_id','path','line_at_scan','element_path','kind','attribute','original','category','reason']);w.writerows(rows)
    print(f'{len(files)} XML files, {len(rows)} contextual candidates: {OUTPUT.name}')
if __name__=='__main__':main()
