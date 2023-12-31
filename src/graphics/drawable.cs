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
    private string path;
    bool isLoaded = false;
    Texture2D texture;

    public Sprite(int width, string path) {
        this.width = width;
        this.path = path;
    }
    
    void LoadTexture() {
        texture = Raylib.LoadTexture(path);
        if (texture.Id == 0)
            throw new FileNotFoundException("Texture not found: " + path);

        width = texture.Width;
        height = texture.Height;
        isLoaded = true;
    }
    
    public void Draw(double x, double y, double scale) {
        if(!isLoaded) LoadTexture();
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