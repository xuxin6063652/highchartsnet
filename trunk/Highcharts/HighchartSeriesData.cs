using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Runtime.Serialization;

namespace Highcharts {
	class HighchartSeriesData {
		[DataMember(EmitDefaultValue = false, Name = "enabled")]
		public List<double> Data { get; set; }
	}
}
