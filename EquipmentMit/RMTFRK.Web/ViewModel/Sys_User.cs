using System;

namespace RMTFRK.Web.ViewModel
{
	/// <summary>
	/// Sys_User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_User
	{
		public Sys_User()
		{}
		#region Model
		private string _userid;
		private string _account;
		private string _password;
		private string _usercname;
		private string _userename;
		private string _email;
		private string _telephone;
		private string _mobile;
		private string _departid;
		private int _sex;
		private string _birthday;
		private string _jobnumber;
		private string _address;
		private string _position;
		private DateTime? _lastlogintime;
		private string _lastloginip;
		private bool? _isenble;
		private bool? _isdelete;
		private bool? _isonline;
		private string _remark;
		private string _creator;
		private DateTime? _createtime;
		/// <summary>
		/// 用户ID，主键
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户帐号
		/// </summary>
		public string Account
		{
			set{ _account=value;}
			get{return _account;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 中文名称
		/// </summary>
		public string UserCName
		{
			set{ _usercname=value;}
			get{return _usercname;}
		}
		/// <summary>
		/// 英文名称
		/// </summary>
		public string UserEName
		{
			set{ _userename=value;}
			get{return _userename;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// 移动电话
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 部门ID
		/// </summary>
		public string DepartID
		{
			set{ _departid=value;}
			get{return _departid;}
		}
		/// <summary>
		/// 性别
		/// </summary>
        public int Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 生日
		/// </summary>
        public string BirthDay
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 工号
		/// </summary>
		public string JobNumber
		{
			set{ _jobnumber=value;}
			get{return _jobnumber;}
		}
		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 职位
		/// </summary>
		public string Position
		{
			set{ _position=value;}
			get{return _position;}
		}
		/// <summary>
		/// 上次登录时间
		/// </summary>
		public DateTime? LastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 上次登录IP
		/// </summary>
		public string LastLoginIP
		{
			set{ _lastloginip=value;}
			get{return _lastloginip;}
		}
		/// <summary>
		/// 是否可用（0-不可用，1-可用）
		/// </summary>
		public bool? IsEnble
		{
			set{ _isenble=value;}
			get{return _isenble;}
		}
		/// <summary>
		/// 是否删除（0-否，1-是）
		/// </summary>
		public bool? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 是否在线（0-否，1-是）
		/// </summary>
		public bool? IsOnline
		{
			set{ _isonline=value;}
			get{return _isonline;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 创建者
		/// </summary>
		public string Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

