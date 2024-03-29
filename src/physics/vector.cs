using System.Text.Json.Serialization;

class Vector {
    public List<double> values = [];

    public Vector(params double[] values) {
        this.values = values.ToList();
    }

    [JsonIgnore]
    public double x {
        get { return values[0]; }
        set { values[0] = value; }
    }

    [JsonIgnore]
    public double y {
        get { return values[1]; }
        set { values[1] = value; }
    }
    
    [JsonIgnore]
    public double z {
        get { return values[2]; }
        set { values[2] = value; }
    }

    static public Vector operator +(Vector a, Vector b) {
        if(a.values.Count != b.values.Count) 
            throw new System.Exception("Vectors must be of the same length");

        var result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(a.values[i] + b.values[i]);

        return result;
    }

    static public Vector operator -(Vector a, Vector b) {
        if(a.values.Count != b.values.Count) 
            throw new System.Exception("Vectors must be of the same length");

        var result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(a.values[i] - b.values[i]);

        return result;
    }

    static public Vector operator -(Vector a) {
        var result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(-a.values[i]);

        return result;
    }

    static public double operator *(Vector a, Vector b) {
        // dot product
        if(a.values.Count != b.values.Count) 
            throw new System.Exception("Vectors must be of the same length");

        double result = 0;
        for(int i = 0; i < a.values.Count; i++)
            result += a.values[i] * b.values[i];

        return result;
    }

    static public Vector operator *(Vector a, double b) {
        var result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(a.values[i] * b);

        return result;
    }

    static public Vector operator *(double a, Vector b) {
        var result = new Vector();
        for(int i = 0; i < b.values.Count; i++)
            result.values.Add(a * b.values[i]);

        return result;
    }

    static public Vector operator /(Vector a, double b) {
        var result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(a.values[i] / b);

        return result;
    }

    static public Vector operator /(double a, Vector b) {
        var result = new Vector();
        for(int i = 0; i < b.values.Count; i++)
            result.values.Add(a / b.values[i]);

        return result;
    }

    public double Length {
        get {
            // return Math.Sqrt(values.Select(d => d*d).Sum());
            return Math.Sqrt(values.Sum(d => d*d));
        }
        
        
    }

    public Vector Unit() {
        return this / this.Length;
    }

    public Vector Rotate(double theta) {
        if(values.Count != 2)
            throw new System.Exception("Can only rotate 2D vectors");

        double x = this.x * Math.Cos(theta) - this.y * Math.Sin(theta);
        double y = this.x * Math.Sin(theta) + this.y * Math.Cos(theta);
        
        return new Vector(x, y);
    }

    public double Angle() {
        return Math.Atan(this.y / this.x);
    }

    public Vector SetAngle(double theta) {
        return new Vector(
            Length * Math.Cos(theta),
            Length * Math.Sin(theta)
        );
    }

    public override string ToString() {
        return this.ToString(4);
    }

    public string ToString(int digits) {
        string result = "<";
        foreach(double value in values) {
            if(value > 0) digits++;
            result += value.ToString($"F{digits}") + ", ";
            if(value > 0) digits--;
        }

        return result.Substring(0, result.Length - 2) + ">";
    }
}