class Vector : List<double> {
    public Vector(params double[] values) {
        foreach (double value in values) {
            this.Add(value);
        }
    }

    public double x {
        get { return (double)this[0]; }
        set { this[0] = value; }
    }

    public double y {
        get { return (double)this[1]; }
        set { this[1] = value; }
    }
    
    public double z {
        get { return (double)this[2]; }
        set { this[2] = value; }
    }

    static public Vector operator +(Vector a, Vector b) {
        if(a.Count != b.Count) 
            throw new System.Exception("Vectors must be of the same length");

        Vector result = new Vector();
        for(int i = 0; i < a.Count; i++)
            result.Add(a[i] + b[i]);

        return result;
    }

    static public Vector operator -(Vector a, Vector b) {
        if(a.Count != b.Count) 
            throw new System.Exception("Vectors must be of the same length");

        Vector result = new Vector();
        for(int i = 0; i < a.Count; i++)
            result.Add(a[i] - b[i]);

        return result;
    }

    static public Vector operator -(Vector a) {
        Vector result = new Vector();
        for(int i = 0; i < a.Count; i++)
            result.Add(-a[i]);

        return result;
    }

    static public double operator *(Vector a, Vector b) {
        // dot product
        if(a.Count != b.Count) 
            throw new System.Exception("Vectors must be of the same length");

        double result = 0;
        for(int i = 0; i < a.Count; i++)
            result += a[i] * b[i];

        return result;
    }

    static public Vector operator *(Vector a, double b) {
        Vector result = new Vector();
        for(int i = 0; i < a.Count; i++)
            result.Add(a[i] * b);

        return result;
    }

    static public Vector operator *(double a, Vector b) {
        Vector result = new Vector();
        for(int i = 0; i < b.Count; i++)
            result.Add(a * b[i]);

        return result;
    }

    static public Vector operator /(Vector a, double b) {
        Vector result = new Vector();
        for(int i = 0; i < a.Count; i++)
            result.Add(a[i] / b);

        return result;
    }

    static public Vector operator /(double a, Vector b) {
        Vector result = new Vector();
        for(int i = 0; i < b.Count; i++)
            result.Add(a / b[i]);

        return result;
    }

    public double Size() {
        double result = 0;
        foreach(double value in this)
            result += value * value;

        return System.Math.Sqrt(result);
    }

    public Vector Normal() {
        return this / this.Size();
    }

    public Vector Rotate(double theta) {
        if(this.Count != 2)
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
        foreach(double value in this) {
            if(value > 0)
                result += " ";
            result += value.ToString($"F{digits}") + ", ";
        }

        return result.Substring(0, result.Length - 2) + ">";
    }

}