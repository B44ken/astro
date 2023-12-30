using Raylib_cs;

class Text : Drawable {
    public System.Drawing.Color color = System.Drawing.Color.White;
    public string text;
    public double size = 24;

    public void Draw(double x, double y, double scale) {
        var rayColor = new Raylib_cs.Color(color.R, color.G, color.B, color.A);
        Raylib.DrawText(text, (int)x, (int)y, (int)size, rayColor);
    }
}