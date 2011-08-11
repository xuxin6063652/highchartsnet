using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Drawing;

namespace Highcharts {
	public enum ChartType {
		Line,
		Spline,
		Area,
		Areaspline,
		Column,
		Bar,
		Pie,
		Scatter
	}

	public enum ChartZoomType {
		None,
		X,
		Y,
		XY
	}

	public enum ChartLegendLayoutType {
		Horizontal,
		Vertical
	}

	public enum ChartAlignType {
		Center,
		Left,
		Right
	}

	public enum ChartTitleAlignType {
		Middle,
		Low,
		High
	}

	public enum ChartVerticalAlignType {
		Top,
		Middle,
		Bottom
	}

	public enum ChartDashStyles {
		Solid,
		ShortDash,
		ShortDot,
		ShortDashDot,
		ShortDashDotDot,
		Dot,
		Dash,
		LongDash,
		DashDot,
		LongDashDot,
		LongDashDotDot
	}

	[DataContract]
	public class Highchart {
		[DataMember(EmitDefaultValue = false, Name = "chart")]
		public HighchartChart Chart { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "title")]
		public HighchartTitle Title { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "subtitle")]
		public HighchartSubtitle Subtitle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "yAxis")]
		public HighchartYAxis YAxis { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "xAxis")]
		public HighchartXAxis XAxis { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "credits")]
		public HighchartCredits Credits { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "legend")]
		public HighchartLegend Legend { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "series")]
		public List<HighchartSeries> Series { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotOptions")]
		public HighchartPlotOptions PlotOptions { get; set; }

		public string Render() {
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Highchart));
			MemoryStream ms = new MemoryStream();
			serializer.WriteObject(ms, this);
			string output = Encoding.UTF8.GetString(ms.ToArray());
			ms.Close();
			return output;
		}
	}

	[DataContract]
	public class HighchartPlotOptions {
		[DataMember(EmitDefaultValue = false, Name = "area")]
		public HighchartPlotOptionsArea Area { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "areaspline")]
		public HighchartPlotOptionsAreaSpline AreaSpline { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "bar")]
		public HighchartPlotOptionsBar Bar { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "column")]
		public HighchartPlotOptionsColumn Column { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "line")]
		public HighchartPlotOptionsLine Line { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "pie")]
		public HighchartPlotOptionsPie Pie { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "series")]
		public HighchartPlotOptionsSeries Series { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "scatter")]
		public HighchartPlotOptionsScatter Scatter { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "spline")]
		public HighchartPlotOptionsSpline Spline { get; set; }
	}

	[DataContract]
	public class HighchartPlotOptionsAreaSpline : HighchartPlotOptionsArea {
	}

	[DataContract]
	public class HighchartPlotOptionsArea : HighchartPlotOptionsSeries {
		[DataMember(EmitDefaultValue = false)]
		private string fillColor { get; set; }
		[IgnoreDataMember]
		public Color FillColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "fillOpacity")]
		public double FillOpacity { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string lineColor { get; set; }
		[IgnoreDataMember]
		public Color LineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "threshold")]
		public int Threshold { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (FillColor != Color.Empty) {
				fillColor = System.Drawing.ColorTranslator.ToHtml(FillColor);
			}
			if (LineColor != Color.Empty) {
				lineColor = System.Drawing.ColorTranslator.ToHtml(LineColor);
			}
		}
	}

	[DataContract]
	public class HighchartPlotOptionsBar : HighchartPlotOptionsColumn {
	}

	[DataContract]
	public class HighchartPlotOptionsColumn : HighchartPlotOptionsSeries {
		[DataMember(EmitDefaultValue = false)]
		private string borderColor { get; set; }
		[IgnoreDataMember]
		public Color BorderColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "borderRadius")]
		public int BorderRadius { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "borderWidth")]
		public int BorderWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "colorByPoint")]
		public bool ColorByPoint { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "groupPadding")]
		public double GroupPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minPointLength")]
		public int MinPointLength { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "pointPadding")]
		public double PointPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "pointWidth")]
		public int PointWidth { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (BorderColor != Color.Empty) {
				borderColor = System.Drawing.ColorTranslator.ToHtml(BorderColor);
			}
		}
	}

	[DataContract]
	public class HighchartPlotOptionsLine : HighchartPlotOptionsSeries {
		[DataMember(EmitDefaultValue = false, Name = "step")]
		public bool Step { get; set; }
	}

	[DataContract]
	public class HighchartPlotOptionsPie : HighchartPlotOptionsSeries {
		[DataMember(EmitDefaultValue = false, Name = "size")]
		public int Size { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "slicedOffset")]
		public int SlicedOffset { get; set; }
	}

	[DataContract]
	public class HighchartPlotOptionsScatter : HighchartPlotOptionsSeries {
	}

	[DataContract]
	public class HighchartPlotOptionsSpline : HighchartPlotOptionsSeries {
	}

	[DataContract]
	public class HighchartPlotOptionsSeries {
		[DataMember(EmitDefaultValue = false, Name = "allowPointSelect")]
		public bool AllowPointSelect { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "animation")]
		public bool Animation { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string color { get; set; }
		[IgnoreDataMember]
		public Color Color { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "cursor")]
		public string Cursor { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string dashStyle { get; set; }
		[IgnoreDataMember]
		public ChartDashStyles DashStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "dataLabels")]
		public HighchartXAxisLabels DataLabels { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "enableMouseTracking")]
		public bool EnableMouseTracking { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "events")]
		public string Events { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "id")]
		public string Id { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "lineWidth")]
		public double LineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "marker")]
		public string Marker { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "point")]
		public string Point { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "pointStart")]
		public int PointStart { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "pointInterval")]
		public int PointInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "selected")]
		public bool Selected { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "shadow")]
		public bool Shadow { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showCheckbox")]
		public bool ShowCheckbox { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showInLegend")]
		public bool ShowInLegend { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "stacking")]
		public string Stacking { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "states")]
		public string States { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "stickyTracking")]
		public bool StickyTracking { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "visible")]
		public bool Visible { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "zIndex")]
		public int ZIndex { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Color != Color.Empty) {
				color = System.Drawing.ColorTranslator.ToHtml(Color);
			}
			if (DashStyle != ChartDashStyles.Solid) {
				dashStyle = DashStyle.ToString().ToLower();
			}
		}
	}

	[DataContract]
	public class HighchartTitle {
		[DataMember(EmitDefaultValue = false)]
		private string align { get; set; }
		[IgnoreDataMember]
		public ChartAlignType Align { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "floating")]
		public string Floating { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "margin")]
		public double Margin { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "text")]
		public string Text { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string verticalAlign { get; set; }
		[IgnoreDataMember]
		public ChartVerticalAlignType VerticalAlign { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "x")]
		public double X { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "y")]
		public double Y { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Align != ChartAlignType.Center) {
				align = Align.ToString().ToLower();
			}
			if (VerticalAlign != ChartVerticalAlignType.Top) {
				verticalAlign = VerticalAlign.ToString().ToLower();
			}
		}
	}

	[DataContract]
	public class HighchartLegend {
		[DataMember(EmitDefaultValue = false)]
		private string align { get; set; }
		[IgnoreDataMember]
		public ChartAlignType Align { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string backgroundColor { get; set; }
		[IgnoreDataMember]
		public Color BackgroundColor { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string borderColor { get; set; }
		[IgnoreDataMember]
		public Color BorderColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "borderRadius")]
		public double BorderRadius { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "borderWidth")]
		public double? BorderWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "enabled")]
		public bool Enabled { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "floating")]
		public bool Floating { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "itemHiddenStyle")]
		public string ItemHiddenStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "itemHoverStyle")]
		public string ItemHoverStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "itemStyle")]
		public string ItemStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "itemWidth")]
		public double ItemWidth { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string layout { get; set; }
		[IgnoreDataMember]
		public ChartLegendLayoutType Layout { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "labelFormatter")]
		public string LabelFormatter { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "lineHeight")]
		public double LineHeight { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "margin")]
		public double Margin { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "reversed")]
		public bool Reversed { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "shadow")]
		public bool Shadow { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "symbolPadding")]
		public double SymbolPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "symbolWidth")]
		public double SymbolWidth { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string verticalAlign { get; set; }
		[IgnoreDataMember]
		public ChartVerticalAlignType VerticalAlign { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "width")]
		public double Width { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "x")]
		public double X { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "y")]
		public double Y { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Align != ChartAlignType.Center) {
				align = Align.ToString().ToLower();
			}
			if (VerticalAlign != ChartVerticalAlignType.Bottom) {
				verticalAlign = VerticalAlign.ToString().ToLower();
			}
			if (Layout != ChartLegendLayoutType.Horizontal) {
				layout = Layout.ToString().ToLower();
			}
			if (BackgroundColor != Color.Empty) {
				backgroundColor = System.Drawing.ColorTranslator.ToHtml(BackgroundColor);
			}
			if (BorderColor != Color.Empty) {
				borderColor = System.Drawing.ColorTranslator.ToHtml(BorderColor);
			}
		}
	}

	[DataContract]
	public class HighchartXAxisTitle {
		[DataMember(EmitDefaultValue = false)]
		private string align { get; set; }
		[IgnoreDataMember]
		public ChartTitleAlignType Align { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "margin")]
		public double Margin { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "rotation")]
		public double Rotation { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "text")]
		public string Text { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Align != ChartTitleAlignType.Middle) {
				align = Align.ToString().ToLower();
			}
		}
	}
	[DataContract]
	public class HighchartYAxisTitle : HighchartXAxisTitle {
	}

	[DataContract]
	public class HighchartAxisPlotLines {
		[DataMember(EmitDefaultValue = false)]
		private string color { get; set; }
		[IgnoreDataMember]
		public Color Color { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string dashStyle { get; set; }
		[IgnoreDataMember]
		public ChartDashStyles DashStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "events")]
		public string Events { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "id")]
		public string Id { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "label")]
		public string Label { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "value")]
		public double? Value { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "width")]
		public double Width { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "zIndex")]
		public double ZIndex { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Color != Color.Empty) {
				color = System.Drawing.ColorTranslator.ToHtml(Color);
			}
			if (DashStyle != ChartDashStyles.Solid) {
				dashStyle = DashStyle.ToString().ToLower();
			}
		}
	}
	
	[DataContract]
	public class HighchartSubtitle {
		[DataMember(EmitDefaultValue = false)]
		private string align { get; set; }
		[IgnoreDataMember]
		public ChartAlignType Align { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "floating")]
		public string Floating { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "margin")]
		public double Margin { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "text")]
		public string Text { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string verticalAlign { get; set; }
		[IgnoreDataMember]
		public ChartVerticalAlignType VerticalAlign { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "x")]
		public double X { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "y")]
		public double Y { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Align != ChartAlignType.Center) {
				align = Align.ToString().ToLower();
			}
			if (VerticalAlign != ChartVerticalAlignType.Top) {
				verticalAlign = VerticalAlign.ToString().ToLower();
			}
		}
	}

	[DataContract]
	public class HighchartChart {
		[DataMember(EmitDefaultValue = false, Name = "alignTicks")]
		public bool AlignTicks { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "animation")]
		public bool Animation { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string backgroundColor { get; set; }
		[IgnoreDataMember]
		public Color BackgroundColor { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string borderColor { get; set; }
		[IgnoreDataMember]
		public Color BorderColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "borderRadius")]
		public double BorderRadius { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "borderWidth")]
		public double BorderWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "className")]
		public string ClassName { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "events")]
		public HighchartEvents Events { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "height")]
		public double Height { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "ignoreHiddenSeries")]
		public bool IgnoreHiddenSeries { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "inverted")]
		public bool Inverted { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "marginTop")]
		public double MarginTop { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "marginRight")]
		public double MarginRight { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "marginBottom")]
		public double MarginBottom { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "marginLeft")]
		public double MarginLeft { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string plotBackgroundColor { get; set; }
		[IgnoreDataMember]
		public Color PlotBackgroundColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotBackgroundImage")]
		public string PlotBackgroundImage { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string plotBorderColor { get; set; }
		[IgnoreDataMember]
		public Color PlotBorderColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotBorderWidth")]
		public double PlotBorderWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotShadow")]
		public bool PlotShadow { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "reflow")]
		public bool Reflow { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "renderTo")]
		public string RenderTo { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "shadow")]
		public bool Shadow { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showAxes")]
		public bool ShowAxes { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "spacingTop")]
		public double SpacingTop { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "spacingRight")]
		public double SpacingRight { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "spacingBottom")]
		public double SpacingBottom { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "spacingLeft")]
		public double SpacingLeft { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string type { get; set; }
		[IgnoreDataMember]
		public ChartType Type { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "width")]
		public double Width { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private string zoomType { get; set; }
		[IgnoreDataMember]
		public ChartZoomType ZoomType { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Type != ChartType.Line) {
				type = Type.ToString().ToLower();
			}
			if (ZoomType != ChartZoomType.None) {
				zoomType = ZoomType.ToString().ToLower();
			}
			if (BackgroundColor != Color.Empty) {
				backgroundColor = System.Drawing.ColorTranslator.ToHtml(BackgroundColor);
			}
			if (BorderColor != Color.Empty) {
				borderColor = System.Drawing.ColorTranslator.ToHtml(BorderColor);
			}
			if (PlotBackgroundColor != Color.Empty) {
				plotBackgroundColor = System.Drawing.ColorTranslator.ToHtml(PlotBackgroundColor);
			}
			if (PlotBorderColor != Color.Empty) {
				plotBorderColor = System.Drawing.ColorTranslator.ToHtml(PlotBorderColor);
			}
		}
	}

	[DataContract]
	public class HighchartEvents {
		/// <summary>
		/// Javascript function: Fires when a series is added to the chart after load time, using the addSeries method. The this keyword refers to the chart object itself. One parameter, event, is passed to the function. This contains common event information based on jQuery or MooTools depending on which library is used as the base for Highcharts. Through event.options you can access the series options that was passed to the addSeries method. Returning false prevents the series from being added.
		/// </summary>
		[DataMember(EmitDefaultValue = false, Name = "addSeries")]
		public string AddSeries { get; set; }
		/// <summary>
		/// Javascript function: Fires when clicking on the plot background. The this keyword refers to the chart object itself. One parameter, event, is passed to the function. This contains common event information based on jQuery or MooTools depending on which library is used as the base for Highcharts.
		/// </summary>
		[DataMember(EmitDefaultValue = false, Name = "click")]
		public string Click { get; set; }
		/// <summary>
		/// Javascript function: Fires when the chart is finished loading. The this keyword refers to the chart object itself. One parameter, event, is passed to the function. This contains common event information based on jQuery or MooTools depending on which library is used as the base for Highcharts.
		/// </summary>
		[DataMember(EmitDefaultValue = false, Name = "load")]
		public string Load { get; set; }
		/// <summary>
		/// Javascript function: Fires when the chart is redrawn, either after a call to chart.redraw() or after an axis, series or point is modified with the redraw option set to true. The this keyword refers to the chart object itself. One parameter, event, is passed to the function. This contains common event information based on jQuery or MooTools depending on which library is used as the base for Highcharts.
		/// </summary>
		[DataMember(EmitDefaultValue = false, Name = "redraw")]
		public string Redraw { get; set; }
		/// <summary>
		/// Javascript function: Fires when an area of the chart has been selected. Selection is enabled by setting the chart's zoomType. The this keyword refers to the chart object itself. One parameter, event, is passed to the function. This contains common event information based on jQuery or MooTools depending on which library is used as the base for Highcharts. The default action for the selection event is to zoom the chart to the selected area. It can be prevented by calling event.preventDefault().
		/// </summary>
		[DataMember(EmitDefaultValue = false, Name = "selection")]
		public string Selection { get; set; }
	}

	[DataContract]
	public class HighchartSeries {
		[DataMember(EmitDefaultValue = false)]
		private List<double?> data { get; set; }
		[IgnoreDataMember]
		public List<double?> Data { get; set; }
		[IgnoreDataMember]
		public Dictionary<int, int> DataDictionary { get; set; }
		[Obsolete]
		[DataMember(EmitDefaultValue = false, Name = "dataParser")]
		public string DataParser { get; set; }
		[Obsolete]
		[DataMember(EmitDefaultValue = false, Name = "dataURL")]
		public string DataUrl { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "name")]
		public string Name { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "stack")]
		public string Stack { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "type")]
		public string Type { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "xAxis")]
		public bool XAxis { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "yAxis")]
		public bool YAxis { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Data != null) {
				data = new List<double?>();
				data.AddRange(Data);
			}
			if (DataDictionary != null) {
				//data.Add(DataDictionary);
			}
		}
	}

	[DataContract]
	public class HighchartCredits {
		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "position")]
		public string Position { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "href")]
		public string Href { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "text")]
		public string Text { get; set; }
	}

	[DataContract]
	public class HighchartXAxisLabels {
		[DataMember(EmitDefaultValue = false)]
		private string align { get; set; }
		[IgnoreDataMember]
		public ChartAlignType Align { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "enabled")]
		public bool Enabled { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "formatter")]
		public string Formatter { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "rotation")]
		public int Rotation { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "staggerLines")]
		public int StaggerLines { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "step")]
		public int Step { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "style")]
		public string Style { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "x")]
		public int X { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "y")]
		public int Y { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Align != ChartAlignType.Center) {
				align = Align.ToString().ToLower();
			}
		}
	}

	[DataContract]
	public class HighchartXAxis {
		[DataMember(EmitDefaultValue = false, Name = "allowDecimals")]
		public bool AllowDecimals { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "alternateGridColor")]
		public string AlternateGridColor { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private List<string> categories { get; set; }
		[IgnoreDataMember]
		public List<string> Categories { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "dateTimeLabelFormats")]
		public string DateTimeLabelFormats { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "endOnTick")]
		public bool EndOnTick { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "events")]
		public string Events { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "gridLineColor")]
		public string GridLineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "gridLineDashStyle")]
		public string GridLineDashStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "gridLineWidth")]
		public double GridLineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "id")]
		public string Id { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "labels")]
		public HighchartXAxisLabels Labels { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "lineColor")]
		public string LineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "lineWidth")]
		public double LineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "linkedTo")]
		public int LinkedTo { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "max")]
		public double Max { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "maxPadding")]
		public double MaxPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "maxZoom")]
		public double MaxZoom { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "min")]
		public double Min { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorGridLineColor")]
		public string MinorGridLineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorGridLineDashStyle")]
		public string MinorGridLineDashStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorGridLineWidth")]
		public double MinorGridLineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickColor")]
		public string MinorTickColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickInterval")]
		public double MinorTickInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickLength")]
		public double MinorTickLength { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickPosition")]
		public string MinorTickPosition { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickWidth")]
		public double MinorTickWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minPadding")]
		public double MinPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "offset")]
		public double Offset { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "opposite")]
		public bool Opposite { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotBands")]
		public string PlotBands { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotLines")]
		public List<HighchartAxisPlotLines> PlotLines { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "reversed")]
		public bool Reversed { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showFirstLabel")]
		public bool ShowFirstLabel { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showLastLabel")]
		public bool ShowLastLabel { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "startOfWeek")]
		public int StartOfWeek { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "startOnTick")]
		public bool StartOnTick { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickColor")]
		public string TickColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickInterval")]
		public int TickInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickLength")]
		public double TickLength { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickmarkPlacement")]
		public string TickmarkPlacement { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickPixelInterval")]
		public double TickPixelInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickPosition")]
		public string TickPosition { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickWidth")]
		public double TickWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "title")]
		public HighchartXAxisTitle Title { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "type")]
		public string Type { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Categories != null) {
				categories = new List<string>();
				categories.AddRange(Categories);
			}
		}
	}

	[DataContract]
	public class HighchartYAxis {
		[DataMember(EmitDefaultValue = false, Name = "allowDecimals")]
		public bool AllowDecimals { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "alternateGridColor")]
		public string AlternateGridColor { get; set; }
		[DataMember(EmitDefaultValue = false)]
		private List<string> categories { get; set; }
		[IgnoreDataMember]
		public List<string> Categories { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "dateTimeLabelFormats")]
		public string DateTimeLabelFormats { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "endOnTick")]
		public bool EndOnTick { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "events")]
		public string Events { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "gridLineColor")]
		public string GridLineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "gridLineDashStyle")]
		public string GridLineDashStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "gridLineWidth")]
		public double GridLineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "id")]
		public string Id { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "labels")]
		public string Labels { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "lineColor")]
		public string LineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "lineWidth")]
		public double LineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "linkedTo")]
		public int LinkedTo { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "max")]
		public double Max { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "maxPadding")]
		public double MaxPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "maxZoom")]
		public double MaxZoom { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "min")]
		public double Min { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorGridLineColor")]
		public string MinorGridLineColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorGridLineDashStyle")]
		public string MinorGridLineDashStyle { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorGridLineWidth")]
		public double MinorGridLineWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickColor")]
		public string MinorTickColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickInterval")]
		public double MinorTickInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickLength")]
		public double MinorTickLength { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickPosition")]
		public string MinorTickPosition { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minorTickWidth")]
		public double MinorTickWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "minPadding")]
		public double MinPadding { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "offset")]
		public double Offset { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "opposite")]
		public bool Opposite { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotBands")]
		public string PlotBands { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "plotLines")]
		public List<HighchartAxisPlotLines> PlotLines { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "reversed")]
		public bool Reversed { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showFirstLabel")]
		public bool ShowFirstLabel { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "showLastLabel")]
		public bool ShowLastLabel { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "startOfWeek")]
		public int StartOfWeek { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "stackLabels")]
		public string StackLabels { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "startOnTick")]
		public bool StartOnTick { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickColor")]
		public string TickColor { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickInterval")]
		public int TickInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickLength")]
		public double TickLength { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickmarkPlacement")]
		public string TickmarkPlacement { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickPixelInterval")]
		public double TickPixelInterval { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickPosition")]
		public string TickPosition { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "tickWidth")]
		public double TickWidth { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "title")]
		public HighchartYAxisTitle Title { get; set; }
		[DataMember(EmitDefaultValue = false, Name = "type")]
		public string Type { get; set; }

		[OnSerializing]
		private void OnSerializing(StreamingContext sc) {
			if (Categories != null) {
				categories = new List<string>();
				categories.AddRange(Categories);
			}
		}
	}
}
