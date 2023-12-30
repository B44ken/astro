class Menu() {
    public Action<int>? onSelect;
    public string title;
    public List<string> options;
    int selected = 0;
    
    public void Draw() {
        Console.Clear();
        Console.WriteLine("  " + title + "\n");
        for(var i = 0; i < options.Count; i++) {
            if(i == selected) {
                Console.Write("> ");
            } else {
                Console.Write("  ");
            }
            Console.WriteLine(options[i]);
            Console.ResetColor();
        }
    }
}