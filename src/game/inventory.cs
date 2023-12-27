class Inventory() {
    public Dictionary<string, double> items = [];

    public void Add(string item, double amount) {
        if(!items.ContainsKey(item)) items[item] = 0;
        items[item] += amount;
    }

    public bool Buy(string item, double cost) {
        if(items[item] < cost) return false;
        items[item] -= cost;
        return true;
    }
    
    public int Get(string item) {
        if(!items.ContainsKey(item)) return 0;
        return (int)items[item];
    }
}