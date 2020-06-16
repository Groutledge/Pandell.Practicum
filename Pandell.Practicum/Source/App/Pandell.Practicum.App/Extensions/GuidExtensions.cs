using System;

namespace Pandell.Practicum.App.Extensions
{
    public static class GuidExtensions
    {
        public static byte[] FromGuid(this Guid guidToTransform)
        {
            return guidToTransform.ToByteArray();
        }

        public static Guid ToGuid(this byte[] bytesToTransform)
        {
            return new Guid(bytesToTransform);
        }
        
        public static bool IsEmptyGuid(this Guid guidToCheckOn)
        {
            return guidToCheckOn == Guid.Empty;
        }
    }
}