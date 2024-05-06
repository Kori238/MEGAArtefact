using MyMathLibrary;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Assertions;

public class TestMyVector3 : MonoBehaviour
{
public void TestTranslate()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix.Translate(new MyVector3(1, 2, 3));
    Assert.AreEqual(matrix[3, 0], 1);
    Assert.AreEqual(matrix[3, 1], 2);
    Assert.AreEqual(matrix[3, 2], 3);
}
public void TestGetPosition()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix[3, 0] = 1;
    matrix[3, 1] = 2;
    matrix[3, 2] = 3;
    MyVector3 position = matrix.GetPosition();
    Assert.AreEqual(position.x, 1);
    Assert.AreEqual(position.y, 2);
    Assert.AreEqual(position.z, 3);
}

public void TestRotate()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    MyMathLibrary.MyQuaternion rotation = new MyMathLibrary.MyQuaternion(Mathf.PI / 2, new MyVector3(1, 0, 0)); // Rotate 90 degrees around x-axis
    Debug.Log(rotation.ToMatrix());
    matrix.Rotate(rotation);
    Assert.AreEqual(matrix[0, 0], 1);
    Assert.AreEqual(matrix[1, 1], 0);
    Assert.AreEqual(matrix[1, 2], -1);
    Assert.AreEqual(matrix[2, 1], 1);
    Assert.AreEqual(matrix[2, 2], 0);
}

public void TestGetRotation()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix[0, 0] = 1;
    matrix[1, 1] = 0;
    matrix[1, 2] = -1;
    matrix[2, 1] = 1;
    matrix[2, 2] = 0;
    MyMathLibrary.MyQuaternion rotation = matrix.GetRotation();
    Debug.Log($"{rotation.w}, {rotation.x}, {rotation.y}, {rotation.z}");
    Assert.AreEqual(rotation.w, Mathf.Cos(Mathf.PI / 4));
    Assert.AreEqual(rotation.x, -Mathf.Sin(Mathf.PI / 4));
    Assert.AreEqual(rotation.y, 0);
    Assert.AreEqual(rotation.z, 0);
}
public void TestScale()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix.Scale(new MyVector3(2, 3, 4));
    Assert.AreEqual(matrix[0, 0], 2);
    Assert.AreEqual(matrix[1, 1], 3);
    Assert.AreEqual(matrix[2, 2], 4);
}

public void TestGetScale()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix[0, 0] = 2;
    matrix[1, 1] = 3;
    matrix[2, 2] = 4;
    MyVector3 scale = matrix.GetScale();
    Assert.AreEqual(scale.x, 2);
    Assert.AreEqual(scale.y, 3);
    Assert.AreEqual(scale.z, 4);
}

public void TestLookAt()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix.LookAt(new MyVector3(0, 0, 1), new MyVector3(0, 1, 0));
    Debug.Log(matrix);
    Assert.AreEqual(matrix[0, 2], 0);
    Assert.AreEqual(matrix[1, 2], 0);
    Assert.AreEqual(matrix[2, 2], -1);
}

public void TestTransformPoint()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix[3, 0] = 1;
    matrix[3, 1] = 2;
    matrix[3, 2] = 3;
    MyVector3 point = matrix.TransformPoint(new MyVector3(4, 5, 6));
    Assert.AreEqual(point.x, 5);
    Assert.AreEqual(point.y, 7);
    Assert.AreEqual(point.z, 9);
}

public void TestInverseTransformPoint()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix[3, 0] = 1;
    matrix[3, 1] = 2;
    matrix[3, 2] = 3;
    MyVector3 point = matrix.InverseTransformPoint(new MyVector3(5, 7, 9));
    Assert.AreEqual(point.x, 4);
    Assert.AreEqual(point.y, 5);
    Assert.AreEqual(point.z, 6);
}

public void TestInverse()
{
    MyMathLibrary.MyMatrix4x4 matrix = new MyMathLibrary.MyMatrix4x4(
        new Vector4(1, 0, 0, 5),
        new Vector4(0, 1, 0, 6),
        new Vector4(0, 0, 1, 7),
        new Vector4(0, 0, 0, 1)
    );
    MyMathLibrary.MyMatrix4x4 inverse = matrix.Inverse();
    Debug.Log(inverse);
    MyMathLibrary.MyMatrix4x4 product = matrix * inverse;
    Debug.Log(product + "Should be Identity");
}

public void TestToQuaternion()
{
    MyMathLibrary.MyMatrix4x4 matrix = MyMathLibrary.MyMatrix4x4.identity;
    matrix[0, 0] = 1;
    matrix[1, 1] = 0;
    matrix[1, 2] = -1;
    matrix[2, 1] = 1;
    matrix[2, 2] = 0;
    MyMathLibrary.MyQuaternion MyQuaternion = matrix.ToQuaternion();
    Assert.AreEqual(MyQuaternion.w, Mathf.Cos(Mathf.PI / 4));
    Assert.AreEqual(MyQuaternion.x, Mathf.Sin(Mathf.PI / 4));
    Assert.AreEqual(MyQuaternion.y, 0);
    Assert.AreEqual(MyQuaternion.z, 0);
}
    void Start()
    {
        TestTranslate();
        TestGetPosition();
        //TestRotate();
        TestGetRotation();
        TestScale();
        TestGetScale();
        TestLookAt();
        TestTransformPoint();
        TestInverse();
        TestInverseTransformPoint();
        
        TestToQuaternion();
    }
}