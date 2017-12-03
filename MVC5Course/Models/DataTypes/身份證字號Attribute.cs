using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.DataTypes
{
    public class 身份證字號Attribute : DataTypeAttribute
    {

        public 身份證字號Attribute() : base(DataType.Text)   //文字類型 輸入
        {

        }

        //打 ov + TAB + 空白鍵 ---> 選擇 可 override function
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            string str = (string)value;
            if (str.Contains("Will"))
                return true;
            else
                return false;
            return base.IsValid(value);
        }


    }
}