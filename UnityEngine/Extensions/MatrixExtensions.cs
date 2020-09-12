namespace UnityEngine
{
    public static class MatrixExtensions
    {
        public static void Deconstruct(this Matrix4x4 self,
                                       out Vector4 column0, out Vector4 column1,
                                       out Vector4 column2, out Vector4 column3)
        {
            column0 = self.GetColumn(0); column1 = self.GetColumn(1);
            column2 = self.GetColumn(2); column3 = self.GetColumn(3);
        }

        public static void Deconstruct(in this Matrix4x4 self,
                                       out float m00, out float m01, out float m02, out float m03,
                                       out float m10, out float m11, out float m12, out float m13,
                                       out float m20, out float m21, out float m22, out float m23,
                                       out float m30, out float m31, out float m32, out float m33)
        {
            m00 = self.m00; m01 = self.m01; m02 = self.m02; m03 = self.m03;
            m10 = self.m10; m11 = self.m11; m12 = self.m12; m13 = self.m13;
            m20 = self.m20; m21 = self.m21; m22 = self.m22; m23 = self.m23;
            m30 = self.m30; m31 = self.m31; m32 = self.m32; m33 = self.m33;
        }

        public static Matrix4x4 With(this Matrix4x4 self,
                                     in Vector4? column0 = null, in Vector4? column1 = null,
                                     in Vector4? column2 = null, in Vector4? column3 = null)
            => new Matrix4x4(
                column0 ?? self.GetColumn(0), column1 ?? self.GetColumn(1),
                column2 ?? self.GetColumn(2), column3 ?? self.GetColumn(3)
            );

        public static Matrix4x4 With(in this Matrix4x4 self,
                                     float? m00 = null, float? m01 = null, float? m02 = null, float? m03 = null,
                                     float? m10 = null, float? m11 = null, float? m12 = null, float? m13 = null,
                                     float? m20 = null, float? m21 = null, float? m22 = null, float? m23 = null,
                                     float? m30 = null, float? m31 = null, float? m32 = null, float? m33 = null)
            => new Matrix4x4 {
                m00 = m00 ?? self.m00, m01 = m01 ?? self.m01, m02 = m02 ?? self.m02, m03 = m03 ?? self.m03,
                m10 = m10 ?? self.m10, m11 = m11 ?? self.m11, m12 = m12 ?? self.m12, m13 = m13 ?? self.m13,
                m20 = m20 ?? self.m20, m21 = m21 ?? self.m21, m22 = m22 ?? self.m22, m23 = m23 ?? self.m23,
                m30 = m30 ?? self.m30, m31 = m31 ?? self.m31, m32 = m32 ?? self.m32, m33 = m33 ?? self.m33
            };
    }
}
