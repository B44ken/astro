using Raylib_cs;

interface Drawable {
    void draw(double x, double y, double scale);
}

class Sprite : Drawable {
    public int width;
    public int height;
    public Texture2D texture;

    public Sprite(int width, string path) {
        texture = Raylib.LoadTexture(path);
        this.width = width;
        this.height = texture.Height * width / texture.Width;
    }

    public void LoadPath(string path) {
        texture = Raylib.LoadTexture(path);
    }

    public void draw(double x, double y, double scale) {
        var screenScale = width * scale / texture.Width;
        var rayPos = new System.Numerics.Vector2((float)(x - width*2), (float)(y - height*2));
        Raylib.DrawTextureEx(texture, rayPos, 0, (float)screenScale, Raylib_cs.Color.WHITE);
    }
}
class Circle : Drawable {
    public double radius;
    public System.Drawing.Color color;

    public Circle(double radius, System.Drawing.Color color) {
        this.radius = radius;
        this.color = color;
    }

    public void draw(double x, double y, double scale) {
        var rayColor = new Raylib_cs.Color(color.R, color.G, color.B, color.A);
        Raylib.DrawCircle((int)x, (int)y, (float)(radius * scale), rayColor);
    }
}