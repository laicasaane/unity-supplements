namespace UnityEngine
{
    public static class MatrixExtensions
    {
        public static void Deconstruct(in this Matrix4x4 self,
                                       out Vector4 column0, out Vector4 column1, out Vector4 column2, out Vector4 column3)
        {
            column0 = self.GetColumn(0);
            column1 = self.GetColumn(1);
            column2 = self.GetColumn(2);
            column3 = self.GetColumn(3);
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
    }
}
