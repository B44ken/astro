using Raylib_cs;

class Menu {
    public Action<int>? onSelect;
    public string title;
    public List<string> options;
    int selected = 0;
    
    public string GetText() {
        string buffer = "";
        buffer += "  " + title + "\n";
        for(var i = 0; i < options.Count; i++) {
            if(i == selected) {
                buffer += "> ";
            } else {
                buffer += "  ";
            }
            buffer += options[i] + "\n";
        }
        return buffer;
    }

    public void HandleMouse() {
        
    }
}