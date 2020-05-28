using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Common.CustomAttributes
{
    /// <summary>
    /// 需要在实现类打标注
    /// </summary>
    public class UnitOfWorkAttribute : Attribute
    {
        public bool Enable { get; set; } = true;

        public UnitOfWorkAttribute(bool enable)
        {
            Enable = enable;
        }
    }
}
