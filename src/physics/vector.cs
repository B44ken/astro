using System.Text.Json.Serialization;

class Vector {
    public List<double> values = [];

    public Vector(params double[] values) {
        this.values = values.ToList();
        // foreach (double value in values) {
        //     this.values.Add(value);
        // }
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

        Vector result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(a.values[i] + b.values[i]);

        return result;
    }

    static public Vector operator -(Vector a, Vector b) {
        if(a.values.Count != b.values.Count) 
            throw new System.Exception("Vectors must be of the same length");

        Vector result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values.Add(a.values[i] - b.values[i]);

        return result;
    }

    static public Vector operator -(Vector a) {
        Vector result = new Vector();
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
        Vector result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values[i] = a.values[i] * b;

        return result;
    }

    static public Vector operator *(double a, Vector b) {
        Vector result = new Vector();
        for(int i = 0; i < b.values.Count; i++)
            result.values[i] = a * b.values[i];

        return result;
    }

    static public Vector operator /(Vector a, double b) {
        Vector result = new Vector();
        for(int i = 0; i < a.values.Count; i++)
            result.values[i] = a.values[i] / b;

        return result;
    }

    static public Vector operator /(double a, Vector b) {
        Vector result = new Vector();
        for(int i = 0; i < b.values.Count; i++)
            result.values[i] = a / b.values[i];

        return result;
    }

    public double Size() {
        double result = 0;
        foreach(double value in values)
            result += value * value;

        return System.Math.Sqrt(result);
    }

    public Vector Unit() {
        return this / this.Size();
    }

    public Vector Rotate(double theta) {
        if(values.Count != 2)
            throw new System.Exception("Can only rotate 2D vectors");

        double x = this.x * Math.Cos(theta) - this.y * Math.Sin(theta);
        double y = this.x * Math.Sin(theta) + this.y * Math.Cos(theta);

        return new Vector(x, y);
    }

    public override string ToString() {
        return this.ToString(4);
    }

    public string ToString(int digits) {
        string result = "<";
        foreach(double value in values) {
            if(value > 0)
                result += " ";
            result += value.ToString($"F{digits}") + ", ";
        }

        return result.Substring(0, result.Length - 2) + ">";
    }
}