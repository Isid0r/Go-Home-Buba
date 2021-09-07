using UnityEngine;

static public class HouseholderTransformation
{
    public static Vector3 TakePosition(this Matrix4x4 m)
    {
        Vector3 position;
        position.x = m.m03;
        position.y = m.m13;
        position.z = m.m23;
        return position;
    }
    public static Quaternion TakeRotation(this Matrix4x4 m)
    {
        Vector3 forward;
        forward.x = m.m02;
        forward.y = m.m12;
        forward.z = m.m22;

        Vector3 upwards;
        upwards.x = m.m01;
        upwards.y = m.m11;
        upwards.z = m.m21;

        return Quaternion.LookRotation(forward, upwards);
    }
    public static Vector3 TakeScale(this Matrix4x4 m)
    {
        Vector3 scale;
        scale.x = new Vector4(m.m00, m.m10, m.m20, m.m30).magnitude;
        scale.y = new Vector4(m.m01, m.m11, m.m21, m.m31).magnitude;
        scale.z = new Vector4(m.m02, m.m12, m.m22, m.m32).magnitude;
        return scale;
    }

    public static void ApplyLocalTRS(this Transform tr, Matrix4x4 trs)
    {
        tr.localPosition = trs.TakePosition();
        tr.localRotation = trs.TakeRotation();
        tr.localScale = trs.TakeScale();
    }
    public static Matrix4x4 TakeLocalTRS(this Transform tr)
    {
        return Matrix4x4.TRS(tr.localPosition, tr.localRotation, tr.localScale);
    }

    public static Matrix4x4 MutiplyByNumber(this Matrix4x4 m, float num)
    {
        return new Matrix4x4(
            new Vector4(m.m00 * num, m.m10 * num, m.m20 * num, m.m30 * num),
            new Vector4(m.m01 * num, m.m11 * num, m.m21 * num, m.m31 * num),
            new Vector4(m.m02 * num, m.m12 * num, m.m22 * num, m.m32 * num),
            new Vector4(m.m03 * num, m.m13 * num, m.m23 * num, m.m33 * num)
        );
    }
    public static Matrix4x4 DivideByNumber(this Matrix4x4 m, float num)
    {
        return new Matrix4x4(
            new Vector4(m.m00 / num, m.m10 / num, m.m20 / num, m.m30 / num),
            new Vector4(m.m01 / num, m.m11 / num, m.m21 / num, m.m31 / num),
            new Vector4(m.m02 / num, m.m12 / num, m.m22 / num, m.m32 / num),
            new Vector4(m.m03 / num, m.m13 / num, m.m23 / num, m.m33 / num)
        );
    }
    public static Matrix4x4 Plus(this Matrix4x4 m, Matrix4x4 mToAdding)
    {
        return new Matrix4x4(
            new Vector4(m.m00 + mToAdding.m00, m.m10 + mToAdding.m10,
                m.m20 + mToAdding.m20, m.m30 + mToAdding.m30),
            new Vector4(m.m01 + mToAdding.m01, m.m11 + mToAdding.m11,
                m.m21 + mToAdding.m21, m.m31 + mToAdding.m31),
            new Vector4(m.m02 + mToAdding.m02, m.m12 + mToAdding.m12,
                m.m22 + mToAdding.m22, m.m32 + mToAdding.m32),
            new Vector4(m.m03 + mToAdding.m03, m.m13 + mToAdding.m13,
                m.m23 + mToAdding.m23, m.m33 + mToAdding.m33)
        );
    }
    public static Matrix4x4 Minus(this Matrix4x4 m, Matrix4x4 mToMinus)
    {
        return new Matrix4x4(
            new Vector4(m.m00 - mToMinus.m00, m.m10 - mToMinus.m10,
                m.m20 - mToMinus.m20, m.m30 - mToMinus.m30),
            new Vector4(m.m01 - mToMinus.m01, m.m11 - mToMinus.m11,
                m.m21 - mToMinus.m21, m.m31 - mToMinus.m31),
            new Vector4(m.m02 - mToMinus.m02, m.m12 - mToMinus.m12,
                m.m22 - mToMinus.m22, m.m32 - mToMinus.m32),
            new Vector4(m.m03 - mToMinus.m03, m.m13 - mToMinus.m13,
                m.m23 - mToMinus.m23, m.m33 - mToMinus.m33)
        );
    }
    public static Matrix4x4 MultiplyVectorsTransposed(Vector4 vec4x1, Vector4 vec1x4)
    {

        float[] vectorPoints = new[] { vec4x1.x, vec4x1.y, vec4x1.z, vec4x1.w },
            transposedVectorPoints = new[]
                {vec1x4.x, vec1x4.y, vec1x4.z, vec1x4.w};
        int matrixDimension = vectorPoints.Length;
        float[] values = new float[matrixDimension * matrixDimension];

        for (int i = 0; i < matrixDimension; i++)
        {
            for (int j = 0; j < matrixDimension; j++)
            {
                values[i + j * matrixDimension] = vectorPoints[i] * transposedVectorPoints[j];
            }

        }
        return new Matrix4x4(
            new Vector4(values[0], values[1], values[2], values[3]),
            new Vector4(values[4], values[5], values[6], values[7]),
            new Vector4(values[8], values[9], values[10], values[11]),
            new Vector4(values[12], values[13], values[14], values[15])
        );
    }
    public static Matrix4x4 HouseholderReflection(this Matrix4x4 m, Vector3 planeNormal)
    {
        planeNormal.Normalize();
        Vector4 planeNormal4 = new Vector4(planeNormal.x, planeNormal.y, planeNormal.z, 0);
        Matrix4x4 householderMatrix = Matrix4x4.identity.Minus(
            MultiplyVectorsTransposed(planeNormal4, planeNormal4).MutiplyByNumber(2));
        return householderMatrix * m;
    }
    public static void LocalReflect(this Transform tr, Vector3 planeNormal)
    {
        var trs = tr.TakeLocalTRS();
        var reflected = trs.HouseholderReflection(planeNormal);
        tr.ApplyLocalTRS(reflected);
    }
}
