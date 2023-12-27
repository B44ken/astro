using Raylib_cs;

interface Drawable {
    void Draw(double x, double y, double scale);
}

class Square : Drawable {
    int width;
    int height;
    System.Drawing.Color color;

    public Square(int width, System.Drawing.Color color) {
        this.width = width;
        this.height = width;
        this.color = color;
    }

    public Square(int width, int height, System.Drawing.Color color) {
        this.width = width;
        this.height = height;
        this.color = color;
    }

    public void Draw(double x, double y, double scale) {
        var rayColor = new Color(color.R, color.G, color.B, color.A);
        var screenX = x - (double)width/2 * scale;
        var screenY = y - (double)height/2 * scale;
        Raylib.DrawRectangle((int)screenX, (int)screenY, (int)(width * scale), (int)(height * scale), rayColor);
    }
}

class Sprite : Drawable {
    int width;
    int height;
    Texture2D texture;

    public Sprite(int width, string path) {
        texture = Raylib.LoadTexture(path);
        if(texture.Width == 0) {
            throw new Exception($"Failed to load texture {path}");
        }
        this.width = width;
        this.height = texture.Height * width / texture.Width;
    }

    public void LoadPath(string path) {
        texture = Raylib.LoadTexture(path);
    }

    public void Draw(double x, double y, double scale) {
        var screenScale = width * scale / texture.Width;
        var rayPos = new System.Numerics.Vector2((float)(x - width*scale), (float)(y - height*scale));
        Raylib.DrawTextureEx(texture, rayPos, 0, (float)screenScale, Color.WHITE);
    }
}
class Circle : Drawable {
    double radius;
    System.Drawing.Color color;

    public Circle(double radius, System.Drawing.Color color) {
        this.radius = radius;
        this.color = color;
    }

    public void Draw(double x, double y, double scale) {
        var rayColor = new Color(color.R, color.G, color.B, color.A);
        Raylib.DrawCircle((int)x, (int)y, (float)(radius * scale), rayColor);
    }
}