﻿using System;

namespace Assembly.Metro.Controls.PageTemplates.Games.Components.MetaData
{
	/// <summary>
	///     Base class for number data.
	/// </summary>
	/// <typeparam name="T">The type of number to hold.</typeparam>
	public abstract class RangeNumberData<T> : ValueField
	{
		private string _type;
		private T _min, _max;

		public RangeNumberData(string name, uint offset, long address, string type, T min, T max, uint pluginLine, string tooltip)
			: base(name, offset, address, pluginLine, tooltip)
		{
			_type = type;
			_min = min;
			_max = max;
		}

		public string Type
		{
			get { return _type; }
			set
			{
				_type = value;
				NotifyPropertyChanged("Type");
			}
		}

		public T Min
		{
			get { return _min; }
			set
			{
				_min = value;
				NotifyPropertyChanged("Min");
			}
		}

		public T Max
		{
			get { return _max; }
			set
			{
				_max = value;
				NotifyPropertyChanged("Max");
			}
		}

	}

	/// <summary>
	///     Unsigned 16-bit integer.
	/// </summary>
	public class RangeUint16Data : RangeNumberData<ushort>
	{
		public RangeUint16Data(string name, uint offset, long address, string type, ushort min, ushort max, uint pluginLine, string tooltip)
			: base(name, offset, address, type, min, max, pluginLine, tooltip)
		{
		}

		public override void Accept(IMetaFieldVisitor visitor)
		{
			visitor.VisitRangeUint16(this);
		}

		public override MetaField CloneValue()
		{
			return new RangeUint16Data(Name, Offset, FieldAddress, Type, Min, Max, PluginLine, ToolTip);
		}
	}

	/// <summary>
	///     32-bit floating-point number.
	/// </summary>
	public class RangeFloat32Data : RangeNumberData<float>
	{
		public RangeFloat32Data(string name, uint offset, long address, string type, float min, float max, uint pluginLine, string tooltip)
			: base(name, offset, address, type, min, max, pluginLine, tooltip)
		{
		}

		public override void Accept(IMetaFieldVisitor visitor)
		{
			visitor.VisitRangeFloat32(this);
		}

		public override MetaField CloneValue()
		{
			return new RangeFloat32Data(Name, Offset, FieldAddress, Type, Min, Max, PluginLine, ToolTip);
		}
	}

	/// <summary>
	///     32-bit floating-point number, converted from radians to degrees
	/// </summary>
	public class RangeDegreeData : RangeNumberData<float>
	{
		private string _type;
		private float _radianmin, _radianmax;

		public RangeDegreeData(string name, uint offset, long address, string type, float min, float max, uint pluginLine, string tooltip)
			: base(name, offset, address, type, min, max, pluginLine, tooltip)
		{
			_type = type;
			_radianmin = min;
			_radianmax = max;
		}

		public new float Min
		{
			get { return FromRadian(_radianmin); }
			set
			{
				_radianmin = ToRadian(value);
				NotifyPropertyChanged("Min");
			}
		}

		public float RadianMin
		{
			get { return _radianmin; }
			set
			{
				_radianmax = value;
				NotifyPropertyChanged("RadianMin");
				NotifyPropertyChanged("Min");
			}
		}

		public new float Max
		{
			get { return FromRadian(_radianmax); }
			set
			{
				_radianmax = ToRadian(value);
				NotifyPropertyChanged("Max");
			}
		}

		public float RadianMax
		{
			get { return _radianmax; }
			set
			{
				_radianmax = value;
				NotifyPropertyChanged("RadianMax");
				NotifyPropertyChanged("ValueMax");
			}
		}

		public override void Accept(IMetaFieldVisitor visitor)
		{
			visitor.VisitRangeDegree(this);
		}

		public override MetaField CloneValue()
		{
			return new RangeDegreeData(Name, Offset, FieldAddress, Type, RadianMin, RadianMax, PluginLine, ToolTip);
		}
	}
}