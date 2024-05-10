using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;


namespace MyMathLibrary
{
    public struct MyVector2
    {
        public float x;
        public float y;

        public MyVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static MyVector2 Add(MyVector2 a, MyVector2 b)
        {
            return new MyVector2(a.x + b.x, a.y + b.y);
        }

        public static MyVector2 Subtract(MyVector2 a, MyVector2 b)
        {
            return new MyVector2(a.x - b.x, a.y - b.y);
        }

        public float Magnitude()
        {
            return Mathf.Sqrt(x * x + y * y);
        }
        public Vector2 ToUnityVector2()
        {
            return new Vector2(x, y);
        }

        public static MyVector2 Multiply(MyVector2 a, float scalar)
        {
            return new MyVector2(a.x * scalar, a.y * scalar);
        } 

        public static MyVector2 Divide(MyVector2 a, float divisor)
        {
            return new MyVector2(a.x / divisor, a.y / divisor);    
        }

        public static float MagnitudeSquared(MyVector2 a)
        {
            return a.x * a.x + a.y * a.y;
        }

        public static MyVector2 Normalize(MyVector2 a) {
            float mag = a.Magnitude();
            if (mag > 0f)  // Avoid division by zero
            {
                return Divide(a, mag);
            }
            else 
            {
                return new MyVector2(0,0); // Return zero vector if magnitude is zero 
            }
        }

        public static float Dot(MyVector2 a, MyVector2 b, bool normalize = false) {
            if (normalize)
            {
                a = Normalize(a);
                b = Normalize(b);
            }
            return a.x * b.x + a.y * b.y;
        }

        public static MyVector2 FromAngle(float angleRadians)
        {
            return new MyVector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        }
        public override string ToString()
        {
            return $"{x}, {y}";
        }
    }
    [Serializable]
    public struct MyVector3
    {
        public float x;
        public float y;
        public float z;

        public MyVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static MyVector3 zero
        {
            get { return new MyVector3(0f, 0f, 0f); }
        }

        public static MyVector3 right
        {
            get { return new MyVector3(1f, 0f, 0f); }
        }

        public static MyVector3 left
        {
            get { return new MyVector3(-1f, 0f, 0f); }
        }

        public static MyVector3 up
        {
            get { return new MyVector3(0f, 1f, 0f); }
        }

        public static MyVector3 down
        {
            get { return new MyVector3(0f, -1f, 0f); }
        }

        public static MyVector3 forward
        {
            get { return new MyVector3(0f, 0f, 1f); }
        }

        public static MyVector3 backward
        {
            get { return new MyVector3(0f, 0f, -1f); }
        }

        // Static Functions
        public static MyVector3 Add(MyVector3 a, MyVector3 b)
        {
            return new MyVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static MyVector3 Subtract(MyVector3 a, MyVector3 b)
        {
            return new MyVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static MyVector3 Clamp(MyVector3 value, MyVector3 min, MyVector3 max)
        {
        return new MyVector3(
            Mathf.Clamp(value.x, min.x, max.x),
            Mathf.Clamp(value.y, min.y, max.y),
            Mathf.Clamp(value.z, min.z, max.z)
        );
        }   

        // Instance Functions
        public float Magnitude()
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }

        public Vector3 ToUnityVector3()
        {
            return new Vector3(x, y, z);
        }

        public static float MagnitudeSquared(MyVector3 a)
        {
            return a.x * a.x + a.y * a.y + a.z * a.z;
        }

        public static MyVector3 Multiply(MyVector3 a, float scalar)
        {
            return new MyVector3(a.x * scalar, a.y * scalar, a.z * scalar);
        }

        public static MyVector3 Multiply(MyVector3 a, MyVector3 b)
        {
            return new MyVector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static MyVector3 Divide(MyVector3 a, float divisor)
        {
            return new MyVector3(a.x / divisor, a.y / divisor, a.z / divisor);
        }

        public static MyVector3 Normalize(MyVector3 a)
        {
            float mag = a.Magnitude();
            if (mag > 0f)  // Avoid division by zero
            {
                return Divide(a, mag);
            }
            else 
            {
                return new MyVector3(0,0,0); // Return zero vector if magnitude is zero 
            }
        }

        public static float Dot(MyVector3 a, MyVector3 b, bool normalize = false)
        {
            if (normalize)
            {
                a = Normalize(a);
                b = Normalize(b);
            }
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

         public static float Angle(MyVector2 a)
        {
            return Mathf.Atan2(a.y, a.x);
        }

        public static MyVector3 FromEuler(float pitch, float yaw, float roll)
        {
            float cp = Mathf.Cos(pitch);
            float sp = Mathf.Sin(pitch);
            float cy = Mathf.Cos(yaw); 
            float sy = Mathf.Sin(yaw);
            float cr = Mathf.Cos(roll);
            float sr = Mathf.Sin(roll);

            return new MyVector3(
                cp * sy,
                -sp,
                cp * cy
            );
        }

        public static MyVector3 Cross(MyVector3 a, MyVector3 b)
        {
            return new MyVector3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
        }

        public static MyVector3 Lerp(MyVector3 a, MyVector3 b, float t)
        {
            return Add(a, Multiply(Subtract(b, a), t));  
        }

        public override string ToString()
        {
            return $"{x}, {y}, {z}";
        }
    }

    public struct MyMatrix4x4
{
    private float[,] elements;

    public MyMatrix4x4(UnityEngine.Vector4 column1, UnityEngine.Vector4 column2, UnityEngine.Vector4 column3, UnityEngine.Vector4 column4)
    {
        elements = new float[4, 4];

        elements[0, 0] = column1.x;
        elements[1, 0] = column1.y;
        elements[2, 0] = column1.z;
        elements[3, 0] = column1.w;

        elements[0, 1] = column2.x;
        elements[1, 1] = column2.y;
        elements[2, 1] = column2.z;
        elements[3, 1] = column2.w;

        elements[0, 2] = column3.x;
        elements[1, 2] = column3.y;
        elements[2, 2] = column3.z;
        elements[3, 2] = column3.w;

        elements[0, 3] = column4.x;
        elements[1, 3] = column4.y;
        elements[2, 3] = column4.z;
        elements[3, 3] = column4.w;
    }

    public MyMatrix4x4(float m00, float m01, float m02, float m03, 
        float m10, float m11, float m12, float m13, 
        float m20, float m21, float m22, float m23, 
        float m30, float m31, float m32, float m33)
        {
            elements = new float[4,4];

            elements[0,0] = m00; elements[0,1] = m01; elements[0,2] = m02; elements[0,3] = m03;
            elements[1,0] = m10; elements[1,1] = m11; elements[1,2] = m12; elements[1,3] = m13;
            elements[2,0] = m20; elements[2,1] = m21; elements[2,2] = m22; elements[2,3] = m23;
            elements[3,0] = m30; elements[3,1] = m31; elements[3,2] = m32; elements[3,3] = m33;
        }

    public float this[int row, int column]
    {
        get { return elements[row, column]; }
        set { elements[row, column] = value; }
    }

    public static MyMatrix4x4 identity
    {
        get
        {
            return new MyMatrix4x4(
                new UnityEngine.Vector4(1, 0, 0, 0),
                new UnityEngine.Vector4(0, 1, 0, 0),
                new UnityEngine.Vector4(0, 0, 1, 0),
                new UnityEngine.Vector4(0, 0, 0, 1));
        }
    }

    public static MyMatrix4x4 operator *(MyMatrix4x4 a, MyMatrix4x4 b)
    {
        MyMatrix4x4 result = new MyMatrix4x4(
            new UnityEngine.Vector4(0, 0, 0, 0),
            new UnityEngine.Vector4(0, 0, 0, 0),
            new UnityEngine.Vector4(0, 0, 0, 0),
            new UnityEngine.Vector4(0, 0, 0, 0));

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }

        return result;
    }

    public static MyMatrix4x4 CreateTranslation(MyVector3 translation)
{
    return new MyMatrix4x4(
        new Vector4(1, 0, 0, translation.x),
        new Vector4(0, 1, 0, translation.y),
        new Vector4(0, 0, 1, translation.z),
        new Vector4(0, 0, 0, 1)
    );
}

public static MyMatrix4x4 CreateRotationX(float angle)
{
    var sin = (float)Math.Sin(angle);
    var cos = (float)Math.Cos(angle);

    return new MyMatrix4x4(
        new Vector4(1, 0, 0, 0),
        new Vector4(0, cos, -sin, 0),
        new Vector4(0, sin, cos, 0),
        new Vector4(0, 0, 0, 1)
    );
}

public static MyMatrix4x4 CreateRotationY(float angle)
{
    var sin = (float)Math.Sin(angle);
    var cos = (float)Math.Cos(angle);

    return new MyMatrix4x4(
        new Vector4(cos, 0, sin, 0),
        new Vector4(0, 1, 0, 0),
        new Vector4(-sin, 0, cos, 0),
        new Vector4(0, 0, 0, 1)
    );
}

public static MyMatrix4x4 CreateRotationZ(float angle)
{
    var sin = (float)Math.Sin(angle);
    var cos = (float)Math.Cos(angle);

    return new MyMatrix4x4(
        new Vector4(cos, -sin, 0, 0),
        new Vector4(sin, cos, 0, 0),
        new Vector4(0, 0, 1, 0),
        new Vector4(0, 0, 0, 1)
    );
}

public static MyMatrix4x4 CreateRotationHamilton(MyQuaternion MyQuaternion)
        {
            return MyQuaternion.ToMatrix();
        }

public static MyMatrix4x4 CreateRotation(MyQuaternion q)
{
    var x = (float)Math.Atan2(2 * (q.w * q.x + q.y * q.z), 1 - 2 * (q.x * q.x + q.y * q.y));
    var y = (float)Math.Asin(2 * (q.w * q.y - q.z * q.x));
    var z = (float)Math.Atan2(2 * (q.w * q.z + q.x * q.y), 1 - 2 * (q.y * q.y + q.z * q.z));

    return CreateRotation(new MyVector3(x, y, z));
}

public static MyMatrix4x4 CreateRotation(MyVector3 euler)
{
    var cosX = (float)Math.Cos(euler.x);
    var sinX = (float)Math.Sin(euler.x);
    var cosY = (float)Math.Cos(euler.y);
    var sinY = (float)Math.Sin(euler.y);
    var cosZ = (float)Math.Cos(euler.z);
    var sinZ = (float)Math.Sin(euler.z);

    return new MyMatrix4x4(
        new Vector4(cosY * cosZ, sinX * sinY * cosZ - cosX * sinZ, cosX * sinY * cosZ + sinX * sinZ, 0),
        new Vector4(cosY * sinZ, sinX * sinY * sinZ + cosX * cosZ, cosX * sinY * sinZ - sinX * cosZ, 0),
        new Vector4(-sinY, sinX * cosY, cosX * cosY, 0),
        new Vector4(0, 0, 0, 1)
    );
}

public static MyMatrix4x4 CreateScale(MyVector3 scale)
{
    return new MyMatrix4x4(
        new Vector4(scale.x, 0, 0, 0),
        new Vector4(0, scale.y, 0, 0),
        new Vector4(0, 0, scale.z, 0),
        new Vector4(0, 0, 0, 1)
    );
}
    public static MyMatrix4x4 InvertTranslationMatrix(MyMatrix4x4 matrix)
    {
        MyMatrix4x4 result = MyMatrix4x4.identity;

        result[0, 3] = -matrix[0, 3];
        result[1, 3] = -matrix[1, 3];
        result[2, 3] = -matrix[2, 3];

        return new MyMatrix4x4(
        new UnityEngine.Vector4(1, 0, 0, -matrix[3, 0]),
        new UnityEngine.Vector4(0, 1, 0, -matrix[3, 1]),
        new UnityEngine.Vector4(0, 0, 1, -matrix[3, 2]),
        new UnityEngine.Vector4(0, 0, 0, 1));
    }

    public static MyMatrix4x4 InvertRotationMatrix(MyMatrix4x4 matrix)
    {
        MyMatrix4x4 result = new MyMatrix4x4(
            new UnityEngine.Vector4(matrix[0, 0], matrix[1, 0], matrix[2, 0], 0),
            new UnityEngine.Vector4(matrix[0, 1], matrix[1, 1], matrix[2, 1], 0),
            new UnityEngine.Vector4(matrix[0, 2], matrix[1, 2], matrix[2, 2], 0),
            new UnityEngine.Vector4(0, 0, 0, 1));

        return result;
    }

    public static MyMatrix4x4 InvertScalingMatrix(MyMatrix4x4 matrix)
    {
        MyMatrix4x4 result = MyMatrix4x4.identity;

        result[0, 0] = 1 / matrix[0, 0];
        result[1, 1] = 1 / matrix[1, 1];
        result[2, 2] = 1 / matrix[2, 2];

        return result;
    }

    public override string ToString()
    {
        var precision = 0.000001f;
        var sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var value = Math.Abs(this[i, j]) < precision ? 0 : this[i, j];
                sb.Append(value.ToString("F6") + " ");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void Translate(MyVector3 translation)
    {
        var translationMatrix = new MyMatrix4x4(
            new UnityEngine.Vector4(1, 0, 0, translation.x),
            new UnityEngine.Vector4(0, 1, 0, translation.y),
            new UnityEngine.Vector4(0, 0, 1, translation.z),
            new UnityEngine.Vector4(0, 0, 0, 1)
        );

        this *= translationMatrix;
    }

    public MyVector3 GetPosition()
    {
        return new MyVector3(this[3, 0], this[3, 1], this[3, 2]);
    }

    public void Rotate(MyQuaternion rotation)
    {
        var rotationMatrix = rotation.ToMatrix();
        this *= rotationMatrix;
    }

    public MyQuaternion GetRotation()
    {
        var rotationMatrix = this.GetRotationMatrix();
        return rotationMatrix.ToQuaternion();
    }

    public void Scale(MyVector3 scale)
    {
        var scaleMatrix = new MyMatrix4x4(
            new UnityEngine.Vector4(scale.x, 0, 0, 0),
            new UnityEngine.Vector4(0, scale.y, 0, 0),
            new UnityEngine.Vector4(0, 0, scale.z, 0),
            new UnityEngine.Vector4(0, 0, 0, 1)
        );

        this *= scaleMatrix;
    }

    public MyVector3 GetScale()
    {
        return new MyVector3(
            new MyVector3(this[0, 0], this[1, 0], this[2, 0]).Magnitude(),
            new MyVector3(this[0, 1], this[1, 1], this[2, 1]).Magnitude(),
            new MyVector3(this[0, 2], this[1, 2], this[2, 2]).Magnitude()
        );
    }

    public void SetPosition(MyVector3 position)
    {
        this[0, 3] = position.x;
        this[1, 3] = position.y;
        this[2, 3] = position.z;
    }

    public void LookAt(MyVector3 target, MyVector3 up)
{
    var position = GetPosition();
    var forward = MyVector3.Normalize(MyVector3.Subtract(target, position));
    var right = MyVector3.Normalize(MyVector3.Cross(forward, up));
    var upVector = MyVector3.Cross(right, forward);

    var rotationMatrix = new MyMatrix4x4(
        new UnityEngine.Vector4(right.x, right.y, right.z, 0),
        new UnityEngine.Vector4(upVector.x, upVector.y, upVector.z, 0),
        new UnityEngine.Vector4(-forward.x, -forward.y, -forward.z, 0),
        new UnityEngine.Vector4(0, 0, 0, 1)
    );

    this *= rotationMatrix;
}

    public MyVector3 TransformPoint(MyVector3 point)
    {
        var result = new MyVector3(
            this[0, 0] * point.x + this[1, 0] * point.y + this[2, 0] * point.z + this[3, 0],
            this[0, 1] * point.x + this[1, 1] * point.y + this[2, 1] * point.z + this[3, 1],
            this[0, 2] * point.x + this[1, 2] * point.y + this[2, 2] * point.z + this[3, 2]
        );

        return result;
    }

    public MyVector3 InverseTransformPoint(MyVector3 point)
    {
        var inverseMatrix = this.Inverse();
        return inverseMatrix.TransformPoint(point);
    }

    public MyMatrix4x4 GetRotationMatrix()
    {
        var scale = GetScale();

        var rotationMatrix = new MyMatrix4x4(
            new UnityEngine.Vector4(this[0, 0] / scale.x, this[0, 1] / scale.y, this[0, 2] / scale.z, 0),
            new UnityEngine.Vector4(this[1, 0] / scale.x, this[1, 1] / scale.y, this[1, 2] / scale.z, 0),
            new UnityEngine.Vector4(this[2, 0] / scale.x, this[2, 1] / scale.y, this[2, 2] / scale.z, 0),
            new UnityEngine.Vector4(0, 0, 0, 1)
        );

        return NormalizeRotationMatrix(rotationMatrix);
    }

    public static MyMatrix4x4 NormalizeRotationMatrix(MyMatrix4x4 matrix)
{
    MyVector3 row0 = new MyVector3(matrix[0,0], matrix[0,1], matrix[0,2]);
    MyVector3 row1 = new MyVector3(matrix[1,0], matrix[1,1], matrix[1,2]);
    MyVector3 row2 = new MyVector3(matrix[2,0], matrix[2,1], matrix[2,2]);

    row0 = MyVector3.Normalize(row0);
    row1 = MyVector3.Normalize(row1);
    row2 = MyVector3.Normalize(row2);

    matrix[0,0] = row0.x;
    matrix[0,1] = row0.y;
    matrix[0,2] = row0.z;

    matrix[1,0] = row1.x;
    matrix[1,1] = row1.y;
    matrix[1,2] = row1.z;

    matrix[2,0] = row2.x;
    matrix[2,1] = row2.y;
    matrix[2,2] = row2.z;

    return matrix;
}

    public bool IsSingular()
{
    float det = Determinant();
    return Mathf.Abs(det) < 0.000001f; // Check if determinant is close to zero
}

private float Determinant()
{
    var m = this;
    float det =
        m[0, 0] * (m[1, 1] * (m[2, 2] * m[3, 3] - m[2, 3] * m[3, 2]) -
                    m[1, 2] * (m[2, 1] * m[3, 3] - m[2, 3] * m[3, 1]) +
                    m[1, 3] * (m[2, 1] * m[3, 2] - m[2, 2] * m[3, 1])) -
        m[0, 1] * (m[1, 0] * (m[2, 2] * m[3, 3] - m[2, 3] * m[3, 2]) -
                    m[1, 2] * (m[2, 0] * m[3, 3] - m[2, 3] * m[3, 0]) +
                    m[1, 3] * (m[2, 0] * m[3, 2] - m[2, 2] * m[3, 0])) +
        m[0, 2] * (m[1, 0] * (m[2, 1] * m[3, 3] - m[2, 3] * m[3, 1]) -
                    m[1, 1] * (m[2, 0] * m[3, 3] - m[2, 3] * m[3, 0]) +
                    m[1, 3] * (m[2, 0] * m[3, 1] - m[2, 1] * m[3, 0])) -
        m[0, 3] * (m[1, 0] * (m[2, 1] * m[3, 2] - m[2, 2] * m[3, 1]) -
                    m[1, 1] * (m[2, 0] * m[3, 2] - m[2, 2] * m[3, 0]) +
                    m[1, 2] * (m[2, 0] * m[3, 1] - m[2, 1] * m[3, 0]));

    return det;
}

    public MyQuaternion ToQuaternion()
    {
        var m = this;

        float qw, qx, qy, qz;
        float trace = m[0, 0] + m[1, 1] + m[2, 2];

        if (trace > 0)
        {
            float s = 0.5f / Mathf.Sqrt(trace + 1);
            qw = 0.25f / s;
            qx = (m[2, 1] - m[1, 2]) * s;
            qy = (m[0, 2] - m[2, 0]) * s;
            qz = (m[1, 0] - m[0, 1]) * s;
        }
        else if (m[0, 0] > m[1, 1] && m[0, 0] > m[2, 2])
        {
            float s = 2 * Mathf.Sqrt(1 + m[0, 0] - m[1, 1] - m[2, 2]);
            qw = (m[2, 1] - m[1, 2]) / s;
            qx = 0.25f * s;
            qy = (m[0, 1] + m[1, 0]) / s;
            qz = (m[0, 2] + m[2, 0]) / s;
        }
        else if (m[1, 1] > m[2, 2])
        {
            float s = 2 * Mathf.Sqrt(1 + m[1, 1] - m[0, 0] - m[2, 2]);
            qw = (m[0, 2] - m[2, 0]) / s;
            qx = (m[0, 1] + m[1, 0]) / s;
            qy = 0.25f * s;
            qz = (m[1, 2] + m[2, 1]) / s;
        }
        else
        {
            float s = 2 * Mathf.Sqrt(1 + m[2, 2] - m[0, 0] - m[1, 1]);
            qw = (m[1, 0] - m[0, 1]) / s;
            qx = (m[0, 2] + m[2, 0]) / s;
            qy = (m[1, 2] + m[2, 1]) / s;
            qz = 0.25f * s;
        }

        return new MyQuaternion(qw, qx, qy, qz);
    }
    
    public MyMatrix4x4 Inverse()
{
    var matrix = new float[4, 8];

    // Initialize the matrix
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            matrix[i, j] = this[i, j];
        }
        matrix[i, 4 + i] = 1;
    }

    // Gaussian elimination
    for (int i = 0; i < 4; i++)
    {
        // Partial pivoting
        int maxRow = i;
        for (int k = i + 1; k < 4; k++)
        {
            if (Math.Abs(matrix[k, i]) > Math.Abs(matrix[maxRow, i]))
            {
                maxRow = k;
            }
        }

        if (maxRow != i)
        {
            for (int j = 0; j < 8; j++)
            {
                var temp = matrix[i, j];
                matrix[i, j] = matrix[maxRow, j];
                matrix[maxRow, j] = temp;
            }
        }

        // Singular matrix check
        if (matrix[i, i] == 0)
        {
            throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
        }

        // Elimination
        for (int j = i + 1; j < 4; j++)
        {
            var factor = matrix[j, i] / matrix[i, i];
            for (int k = 0; k < 8; k++)
            {
                matrix[j, k] -= factor * matrix[i, k];
            }
        }
    }

    // Back substitution
    for (int i = 3; i >= 0; i--)
    {
        for (int j = 3; j >= 0; j--)
        {
            if (i != j)
            {
                var factor = matrix[j, i] / matrix[i, i];
                for (int k = 0; k < 8; k++)
                {
                    matrix[j, k] -= factor * matrix[i, k];
                }
            }
        }

        // Diagonal element normalization
        var invDiag = 1 / matrix[i, i];
        for (int k = 0; k < 8; k++)
        {
            matrix[i, k] *= invDiag;
        }
    }

    // Extract the inverse matrix
    var inverse = MyMatrix4x4.identity;
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            inverse[i, j] = matrix[i, 4 + j];
        }
    }

    return inverse;
}
    }
    [Serializable]
    public struct MyQuaternion
{
    public float w;
    public float x;
    public float y;
    public float z;

    public MyQuaternion(float angle, MyVector3 axis)
    {
        float halfAngle = angle * 0.5f;
        float sinHalfAngle = Mathf.Sin(halfAngle);

        w = Mathf.Cos(halfAngle);
        x = axis.x * sinHalfAngle;
        y = axis.y * sinHalfAngle;
        z = axis.z * sinHalfAngle;
    }
    
    public MyQuaternion(MyVector3 position)
    {
        w = 1;
        x = position.x;
        y = position.y;
        z = position.z;
    }

    public MyQuaternion(float w, float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public Quaternion ToUnityQuaternion()
    {
        return new Quaternion(x, y, z, w); 
    }

    public MyQuaternion Normalize()
    {
        float magnitude = (float)Math.Sqrt(x * x + y * y + z * z + w * w);
        if (magnitude > 0.00001f) // Avoid division by zero
        {
            x /= magnitude;
            y /= magnitude;
            z /= magnitude;
            w /= magnitude;
        }
        return this;
    }

    public MyVector3 ToEulerAngles()
    {
        float qx, qy, qz, qw;

        // Ensure quaternion is normalized
        float norm = w * w + x * x + y * y + z * z;
        if (norm > 0.00001f) // Avoid division by zero
        {
            qw = w / norm;
            qx = x / norm;
            qy = y / norm;
            qz = z / norm;
        }
        else
        {
            qw = w;
            qx = x;
            qy = y;
            qz = z;
        }

        // Calculate Euler angles
        float test = qx * qy + qz * qw;
        float ax, ay, az;
        if (test > 0.499f)
        {
            // Singularity at the pole
            ay = 2 * Mathf.Atan2(qx, qw);
            az = Mathf.PI / 2;
            ax = 0;
        }
        else if (test < -0.499f)
        {
            // Singularity at the pole
            ay = -2 * Mathf.Atan2(qx, qw);
            az = -Mathf.PI / 2;
            ax = 0;
        }
        else
        {
            ay = Mathf.Atan2(2 * qy * qw - 2 * qx * qz, 1 - 2 * qy * qy - 2 * qz * qz);
            az = Mathf.Asin(2 * test);
            ax = Mathf.Atan2(2 * qx * qw - 2 * qy * qz, 1 - 2 * qx * qx - 2 * qz * qz);
        }

        return new MyVector3(ax, ay, az);
    }

    public static MyQuaternion FromEulerAngles(MyVector3 eulerAngles)
    {
        float x = eulerAngles.x / 2;
        float y = eulerAngles.y / 2;
        float z = eulerAngles.z / 2;

        float cx = Mathf.Cos(x);
        float sx = Mathf.Sin(x);
        float cy = Mathf.Cos(y);
        float sy = Mathf.Sin(y);
        float cz = Mathf.Cos(z);
        float sz = Mathf.Sin(z);

        return new MyQuaternion(
            cx * cy * cz + sx * sy * sz,
            sx * cy * cz - cx * sy * sz,
            cx * sy * cz + sx * cy * sz,
            cx * cy * sz - sx * sy * cz
        );
    }

    public static MyQuaternion operator *(MyQuaternion a, MyQuaternion b)
    {
       return new MyQuaternion(
            a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z,
            a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
            a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
            a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w);
    }
    public static MyVector3 operator *(MyQuaternion quat, MyVector3 vec)
    {
        float num = quat.x * 2f;
        float num2 = quat.y * 2f;
        float num3 = quat.z * 2f;
        float num4 = quat.x * num;
        float num5 = quat.y * num2;
        float num6 = quat.z * num3;
        float num7 = quat.x * num2;
        float num8 = quat.x * num3;
        float num9 = quat.y * num3;
        float num10 = quat.w * num;
        float num11 = quat.w * num2;
        float num12 = quat.w * num3;
        MyVector3 result;
        result.x = (1f - (num5 + num6)) * vec.x + (num7 - num12) * vec.y + (num8 + num11) * vec.z;
        result.y = (num7 + num12) * vec.x + (1f - (num4 + num6)) * vec.y + (num9 - num10) * vec.z;
        result.z = (num8 - num11) * vec.x + (num9 + num10) * vec.y + (1f - (num4 + num5)) * vec.z;
        return result;
    }

    public MyQuaternion Inverse()
    {
        float norm = w * w + x * x + y * y + z * z;
        return new MyQuaternion(w / norm, -x / norm, -y / norm, -z / norm);
    }

    public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t)
    {
        float cosHalfTheta = a.w * b.w + a.x * b.x + a.y * b.y + a.z * b.z;
        if (cosHalfTheta < 0)
        {
            b = new MyQuaternion(-b.w, -b.x, -b.y, -b.z);
            cosHalfTheta = -cosHalfTheta;
        }

        if (Mathf.Abs(cosHalfTheta) >= 1)
        {
            return a;
        }

        float halfTheta = Mathf.Acos(cosHalfTheta);
        float sinHalfTheta = Mathf.Sqrt(1 - cosHalfTheta * cosHalfTheta);

        float wa = Mathf.Sin((1 - t) * halfTheta) / sinHalfTheta;
        float wb = Mathf.Sin(t * halfTheta) / sinHalfTheta;

        return new MyQuaternion(
            wa * a.w + wb * b.w,
            wa * a.x + wb * b.x,
            wa * a.y + wb * b.y,
            wa * a.z + wb * b.z);
        }

    public MyMatrix4x4 ToMatrix()
    {
        var x = this.x;
        var y = this.y;
        var z = this.z;
        var w = this.w;

        var xx = x * x;
        var xy = x * y;
        var xz = x * z;
        var xw = x * w;

        var yy = y * y;
        var yz = y * z;
        var yw = y * w;

        var zz = z * z;
        var zw = z * w;

        return new MyMatrix4x4(
            new UnityEngine.Vector4(1 - 2 * (yy + zz), 2 * (xy - zw), 2 * (xz + yw), 0),
            new UnityEngine.Vector4(2 * (xy + zw), 1 - 2 * (xx + zz), 2 * (yz - xw), 0),
            new UnityEngine.Vector4(2 * (xz - yw), 2 * (yz + xw), 1 - 2 * (xx + yy), 0),
            new UnityEngine.Vector4(0, 0, 0, 1)
        );
    }
    public static MyQuaternion LookRotation(MyVector3 forward, MyVector3 upwards)
    {
        // Normalize the forward direction
        forward = MyVector3.Normalize(forward);

        // Calculate the right vector using the cross product of forward and upwards
        MyVector3 right = MyVector3.Normalize(MyVector3.Cross(upwards, forward));

        // Calculate the up vector using the cross product of forward and right
        MyVector3 up = MyVector3.Cross(forward, right);

        // Create a rotation matrix from the right, up, and forward vectors
        MyMatrix4x4 rotationMatrix = new MyMatrix4x4(
            right.x, right.y, right.z, 0,
            up.x, up.y, up.z, 0,
            forward.x, forward.y, forward.z, 0,
            0, 0, 0, 1
        );

        // Convert the rotation matrix to a quaternion
        return rotationMatrix.ToQuaternion();
    }
}
    [Serializable]
    public class MyTransform
{
    public MyVector3 position;
    public MyQuaternion rotation;
    public MyVector3 scale;
    public MyGameObject parent;
    public List<MyGameObject> children = new List<MyGameObject>();
    public MyGameObject gameObject;

    public MyTransform(MyGameObject myGameObject = null, List<MyGameObject> children = null, MyGameObject parent = null)
    {
        position = new MyVector3(0, 0, 0);
        rotation = new MyQuaternion(1, 0, 0, 0); // Identity quaternion
        scale = new MyVector3(1, 1, 1);
        this.gameObject = myGameObject;
        if (children != null) this.children = children;
        this.parent = parent;
    }

    public MyTransform(MyVector3 position, MyQuaternion rotation, MyVector3 scale, 
        MyGameObject myGameObject = null, List<MyGameObject> children = null, MyGameObject parent = null)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.gameObject = myGameObject;
        if (children != null) this.children = children;
        this.parent = parent;
    }

    public MyVector3 forward
    {
        get { return rotation * MyVector3.forward;}
    }

    public MyVector3 backward
    {
        get { return rotation * MyVector3.backward; }
    }

    public MyVector3 up
    {
        get { return rotation * MyVector3.up; }
    }

    public MyVector3 down
    {
        get { return rotation * MyVector3.down; }
    }

    public MyVector3 right
    {
        get { return rotation * MyVector3.right; }
    }

    public MyVector3 left
    {
        get { return rotation * MyVector3.left; }
    }

    public MyGameObject SetParent(MyGameObject gameObject)
    {
        if (this.parent != null)
        {
            this.parent.myTransform.children.Remove(gameObject);
        }
        this.parent = gameObject;
        if (parent != null && this.gameObject != null && !parent.myTransform.children.Contains(this.gameObject))
        {
            return this.gameObject;
        }
        return null;
    }

    public MyMatrix4x4 GetLocalToWorldMatrix()
    {
        MyMatrix4x4 translationMatrix = MyMatrix4x4.CreateTranslation(position);
        MyMatrix4x4 rotationMatrix = MyMatrix4x4.CreateRotation(rotation.Normalize());
        rotationMatrix = MyMatrix4x4.NormalizeRotationMatrix(rotationMatrix);
        MyMatrix4x4 scaleMatrix = MyMatrix4x4.CreateScale(scale);

        MyMatrix4x4 localToWorldMatrix = scaleMatrix * rotationMatrix * translationMatrix;

        if (parent != null)
        {
            localToWorldMatrix = localToWorldMatrix * parent.myTransform.GetLocalToWorldMatrix();
        }

        return localToWorldMatrix;
    }

    public MyMatrix4x4 GetWorldToLocalMatrix()
    {
        MyMatrix4x4 localToWorldMatrix = GetLocalToWorldMatrix();
        return localToWorldMatrix.Inverse();
    }

    public void Translate(MyVector3 translation)
    {
        position = MyVector3.Add(position, translation);
    }

    public void Rotate(MyQuaternion rotation)
    {
        this.rotation = rotation * this.rotation;
    }

    public void Scale(MyVector3 scale)
    {
        this.scale = MyVector3.Multiply(this.scale, scale);
    }

    public void LookAt(MyVector3 target)
    {
        MyVector3 direction = MyVector3.Normalize(MyVector3.Subtract(target, position));
        rotation = MyQuaternion.LookRotation(direction, new MyVector3(0, 1, 0));
    }
}
    } 