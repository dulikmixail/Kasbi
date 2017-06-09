var weekend = [0,6];
var weekendColor = "#e0e0e0";
var fontface = "Verdana";
var fontsize = 2;
var digit = "0123456789";
var gNow = new Date();
var ggWinCal;

isNav = (navigator.appName.indexOf("Netscape") != -1) ? true : false;
isIE = (navigator.appName.indexOf("Microsoft") != -1) ? true : false;

Calendar.Months = ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
"Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"];

// Non-Leap year Month days..
Calendar.DOMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
// Leap year Month days..
Calendar.lDOMonth = [31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];


function Calendar(p_item, p_WinCal, p_format, p_date) {

	if (p_WinCal == null)
		this.gWinCal = ggWinCal;
	else
		this.gWinCal = p_WinCal;

	if (p_date == null) this.gItemDate = new Date();
	else this.gItemDate = new Date(Date.parse(p_date));
	
	this.gYear = this.gItemDate.getFullYear();
	this.gMonth = this.gItemDate.getMonth();
	this.gMonthName = Calendar.get_month(this.gMonth);
	this.gYearly = false;

	this.gFormat = p_format;
	this.gBGColor = "white";
	this.gFGColor = "black";
	this.gTextColor = "black";
	this.gHeaderColor = "black";
	this.gReturnItem = p_item;
}

Calendar.get_month = Calendar_get_month;
Calendar.get_daysofmonth = Calendar_get_daysofmonth;
Calendar.calc_month_year = Calendar_calc_month_year;
Calendar.print = Calendar_print;

function Calendar_get_month(monthNo) {
	return Calendar.Months[monthNo];
}

function Calendar_get_daysofmonth(monthNo, p_year) {
	/* 
	Check for leap year ..
	1.Years evenly divisible by four are normally leap years, except for... 
	2.Years also evenly divisible by 100 are not leap years, except for... 
	3.Years also evenly divisible by 400 are leap years. 
	*/
	if ((p_year % 4) == 0) {
		if ((p_year % 100) == 0 && (p_year % 400) != 0)
			return Calendar.DOMonth[monthNo];
	
		return Calendar.lDOMonth[monthNo];
	} else
		return Calendar.DOMonth[monthNo];
}

function Calendar_calc_month_year(p_Month, p_Year, incr) {
	/* 
	Will return an 1-D array with 1st element being the calculated month 
	and second being the calculated year 
	after applying the month increment/decrement as specified by 'incr' parameter.
	'incr' will normally have 1/-1 to navigate thru the months.
	*/
	var ret_arr = new Array();
	
	if (incr == -1) {
		// B A C K W A R D
		if (p_Month == 0) {
			ret_arr[0] = 11;
			ret_arr[1] = parseInt(p_Year) - 1;
		}
		else {
			ret_arr[0] = parseInt(p_Month) - 1;
			ret_arr[1] = parseInt(p_Year);
		}
	} else if (incr == 1) {
		// F O R W A R D
		if (p_Month == 11) {
			ret_arr[0] = 0;
			ret_arr[1] = parseInt(p_Year) + 1;
		}
		else {
			ret_arr[0] = parseInt(p_Month) + 1;
			ret_arr[1] = parseInt(p_Year);
		}
	}
	
	return ret_arr;
}

function Calendar_print() {
	ggWinCal.print();
}

// This is for compatibility with Navigator 3, we have to create and discard one object before the prototype object exists.
new Calendar();

Calendar.prototype.getMonthlyCalendarCode = function() {
	var vCode = "";
	var vHeader_Code = "";
	var vData_Code = "";
	
	// Begin Table Drawing code here..
	vCode = vCode + "<TABLE BORDER=0 >";
	
	vHeader_Code = this.cal_header();
	vData_Code = this.cal_data();
	vCode = vCode + vHeader_Code + vData_Code;
	
	vCode = vCode + "</TABLE>";
	
	return vCode;
}

Calendar.prototype.show = function() {
	var vCode = "";
	
	this.gWinCal.document.open();

	// Setup the page...
	this.wwrite("<html>");
	this.wwrite("<head><title>Calendar</title>");
	this.wwrite("<link href=\"../styles.css\" type=\"text/css\" rel=\"stylesheet\">");
	this.wwrite("</head>");

	this.wwrite("<body bottomMargin=0 leftMargin=0 topMargin=0 rightMargin=0>");
	/* + 
		"link=\"" + this.gLinkColor + "\" " + 
		"vlink=\"" + this.gLinkColor + "\" " +
		"alink=\"" + this.gLinkColor + "\" " +
		"text=\"" + this.gTextColor + "\">");*/
	this.wwriteA("<TABLE WIDTH=\"100%\" BORDER=0 CLASS=\"PageTitle\"><TR>");
	this.wwriteA("<TD CLASS=\"HeaderPage\">");
	this.wwriteA(this.gMonthName + " " + this.gYear);
	this.wwriteA("</TD></TR></TABLE>");

	// Show navigation buttons
	var prevMMYYYY = Calendar.calc_month_year(this.gMonth, this.gYear, -1);
	var prevMM = prevMMYYYY[0];
	var prevYYYY = prevMMYYYY[1];
	
	var prevMDate = (prevMM + 1) +"/01/" + prevYYYY;
	var prevYDate = (this.gMonth + 1) +"/01/" + (this.gYear - 1);

	var nextMMYYYY = Calendar.calc_month_year(this.gMonth, this.gYear, 1);
	var nextMM = nextMMYYYY[0];
	var nextYYYY = nextMMYYYY[1];
	
	var nextMDate = (nextMM + 1) + "/01/" + nextYYYY;
	var nextYDate = (this.gMonth + 1) + "/01/" + (this.gYear + 1);
	
	this.wwrite("<TABLE  WIDTH='100%' BORDER=0 CELLSPACING=0 CELLPADDING=0><TR><TD CLASS='CalendarButton' WIDTH=\"25%\" ALIGN=center><B>");
	this.wwrite("[&nbsp;<A class =\"lnk\" HREF=\"" +
		"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" + prevYDate + "');" +
		"\">&nbsp;&lt;&lt;&nbsp;<\/A> ]</B></TD><TD CLASS='CalendarButton' WIDTH=\"25%\" ALIGN=center><B>");
	this.wwrite("[&nbsp;<A class =\"lnk\" HREF=\"" +
		"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" + prevMDate + "');" +
		"\">&nbsp;&lt;&nbsp;<\/A> ]</B></TD>");
	this.wwrite("<TD CLASS='CalendarButton' WIDTH=\"25%\" ALIGN=center><B>");
	this.wwrite("[&nbsp;<A class =\"lnk\" HREF=\"" +
		"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" + nextMDate + "');" +
		"\">&nbsp;&gt;&nbsp;<\/A> ]</B></TD><TD CLASS='CalendarButton' WIDTH=\"25%\" ALIGN=center><B>");
	this.wwrite("[&nbsp;<A class =\"lnk\" HREF=\"" +
		"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" + nextYDate + "');" +
		"\">&nbsp;&gt;&gt;&nbsp;<\/A> ]</B></TD></TR></TABLE>");

	// Get the complete calendar code for the month..
	vCode = this.getMonthlyCalendarCode();
	this.wwrite(vCode);

	this.wwriteA("<TABLE WIDTH=\"100%\" BORDER=0><TR>");
	this.wwriteA("<TD CLASS=\"PageTitle\" ALIGN=\"center\">");
	this.wwriteA("<A class =\"lnk\" HREF='#' onClick=\"self.opener.document." + this.gReturnItem + ".value='';window.close();\"><CENTER><FONT COLOR=\"BLACK\">Очистить</FONT></CENTER></A>");
	this.wwriteA("</TD>");
	this.wwriteA("<TD CLASS=\"PageTitle\" ALIGN=\"center\">");
	this.wwriteA("Сегодня <A CLASS=\"PageTitle\" REV='next' HREF=\"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" +(gNow.getMonth() + 1) +"/"+ gNow.getDay() +"/"+ gNow.getFullYear() + "');\">"+gNow.toDateString() + "</A>");
	this.wwriteA("</TD></TR></TABLE>");

	this.wwrite("</body></html>");
	this.gWinCal.document.close();
}

Calendar.prototype.showY = function() {
	var vCode = "";
	var i;
	var vr, vc, vx, vy;		// Row, Column, X-coord, Y-coord
	var vxf = 285;			// X-Factor
	var vyf = 200;			// Y-Factor
	var vxm = 10;			// X-margin
	var vym;				// Y-margin
	if (isIE)	vym = 75;
	else if (isNav)	vym = 25;
	
	this.gWinCal.document.open();

	this.wwrite("<html>");
	this.wwrite("<head><title>Calendar</title>");
	this.wwrite("<style type='text/css'>\n<!--");
	for (i=0; i<12; i++) {
		vc = i % 3;
		if (i>=0 && i<= 2)	vr = 0;
		if (i>=3 && i<= 5)	vr = 1;
		if (i>=6 && i<= 8)	vr = 2;
		if (i>=9 && i<= 11)	vr = 3;
		
		vx = parseInt(vxf * vc) + vxm;
		vy = parseInt(vyf * vr) + vym;

		this.wwrite(".lclass" + i + " {position:absolute;top:" + vy + ";left:" + vx + ";}");
	}
	this.wwrite("-->\n</style>");
	this.wwrite("</head>");

	this.wwrite("<body " + 
		"link=\"" + this.gLinkColor + "\" " + 
		"vlink=\"" + this.gLinkColor + "\" " +
		"alink=\"" + this.gLinkColor + "\" " +
		"text=\"" + this.gTextColor + "\">");
	this.wwrite("<FONT FACE='" + fontface + "' SIZE=2><B>");
	this.wwrite("Year : " + this.gYear);
	this.wwrite("</B><BR>");

	// Show navigation buttons
	var prevYYYY = parseInt(this.gYear) - 1;
	var nextYYYY = parseInt(this.gYear) + 1;
	var prevYDate = this.gMonth + "/01/" + prevYYYY;
	var nextYDate = this.gMonth + "/01/" + nextYYYY;
	
	this.wwrite("<TABLE  WIDTH='100%' BORDER=1 CELLSPACING=0 CELLPADDING=0 BGCOLOR='#e0e0e0'><TR><TD ALIGN=center>");
	this.wwrite("[<A class =\"lnk\" HREF=\"" +
		"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" + prevYDate + "');" +
		"\" alt='Prev Year'><<<\/A>]</TD><TD ALIGN=center>");
	this.wwrite("[<A class =\"lnk\" HREF=\"javascript:window.print();\">Print</A>]</TD><TD ALIGN=center>");
	this.wwrite("[<A class =\"lnk\" HREF=\"" +
		"javascript:window.opener.Build('" + this.gReturnItem + "', '" + this.gFormat + "', '" + nextYDate + "');" +
		"\">>><\/A>]</TD></TR></TABLE><BR>");

	// Get the complete calendar code for each month..
	var j;
	for (i=11; i>=0; i--) {
		if (isIE)
			this.wwrite("<DIV ID=\"layer" + i + "\" CLASS=\"lclass" + i + "\">");
		else if (isNav)
			this.wwrite("<LAYER ID=\"layer" + i + "\" CLASS=\"lclass" + i + "\">");

		this.gMonth = i;
		this.gMonthName = Calendar.get_month(this.gMonth);
		vCode = this.getMonthlyCalendarCode();
		this.wwrite(this.gMonthName + "/" + this.gYear + "<BR>");
		this.wwrite(vCode);

		if (isIE)
			this.wwrite("</DIV>");
		else if (isNav)
			this.wwrite("</LAYER>");
	}

	this.wwrite("</font><BR></body></html>");
	this.gWinCal.document.close();
}

Calendar.prototype.wwrite = function(wtext) {
	this.gWinCal.document.writeln(wtext);
}

Calendar.prototype.wwriteA = function(wtext) {
	this.gWinCal.document.write(wtext);
}

Calendar.prototype.cal_header = function() {
	var vCode = "";
	
	vCode = vCode + "<TR class=\"GridTitle\">";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='14%'><CENTER><B>Вос</B></CENTER></TD>";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='14%'><CENTER><B>Пон</B></CENTER></TD>";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='14%'><CENTER><B>Вт</B></CENTER></TD>";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='14%'><CENTER><B>Ср</B></CENTER></TD>";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='14%'><CENTER><B>Чт</B></CENTER></TD>";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='14%'><CENTER><B>Пят</B></CENTER></TD>";
	vCode = vCode + "<TD class=\"headerGrid\" WIDTH='16%'><CENTER><B>Суб</B></CENTER></TD>";
	vCode = vCode + "</TR>";
	
	return vCode;
}

Calendar.prototype.cal_data = function() {
	var vDate = new Date();
	vDate.setDate(1);
	vDate.setMonth(this.gMonth);
	vDate.setFullYear(this.gYear);

	var vFirstDay=vDate.getDay();
	var vDay=1;
	var vLastDay=Calendar.get_daysofmonth(this.gMonth, this.gYear);
	var vOnLastDay=0;
	var vCode = "";
	
	var vNowDay = gNow.getDate();
	var vNowMonth = gNow.getMonth();
	var vNowYear = gNow.getFullYear();

	/*
	Get day for the 1st of the requested month/year..
	Place as many blank cells before the 1st day of the month as necessary. 
	*/

	vCode = vCode + "<TR class=\"CalendarTableRow\">";
	for (i=0; i<vFirstDay; i++) {
		vCode = vCode + "<TD WIDTH='14%'>&nbsp;</TD>";
	}

	// Write rest of the 1st week
	for (j=vFirstDay; j<7; j++) {
		if (vDay == vNowDay && this.gMonth == vNowMonth && this.gYear == vNowYear)
			vCode = vCode + "<TD CLASS=\"CalendarTableRow\" WIDTH='14%'>";
		else
			vCode = vCode + "<TD WIDTH='14%'>";

		vCode = vCode + "<A class =\"lnk\" HREF='#' " + 
				"onClick=\"self.opener.document." + this.gReturnItem + ".value='" + 
				this.format_data(vDay) + 
				"';window.close();\"><CENTER><FONT COLOR=\"BLACK\">" + 
				this.format_day(vDay) + 
			"</FONT></CENTER></A>" + 
			"</TD>";
		vDay=vDay + 1;
	}
	vCode = vCode + "</TR>";

	// Write the rest of the weeks
	for (k=2; k<7; k++) {
		vCode = vCode + "<TR class=\"CalendarTableRow\">";

		for (j=0; j<7; j++) {
			vCode = vCode + "<TD WIDTH='14%'>" + 
				"<A class =\"lnk\" HREF='#' " + 
					"onClick=\"self.opener.document." + this.gReturnItem + ".value='" + 
					this.format_data(vDay) + 
					"';window.close();\"><CENTER><FONT COLOR=\"BLACK\">" + 
				this.format_day(vDay) + 
				"</FONT></CENTER></A>" + 
				"</TD>";
			vDay=vDay + 1;

			if (vDay > vLastDay) {
				vOnLastDay = 1;
				break;
			}
		}

		if (j == 6)
			vCode = vCode + "</TR>";
		if (vOnLastDay == 1)
			break;
	}
	
	// Fill up the rest of last week with proper blanks, so that we get proper square blocks
	for (m=1; m<(7-j); m++) {
		if (this.gYearly)
			vCode = vCode + "<TD class=\"Caption\" WIDTH='14%'><FONT COLOR='gray'>&nbsp;</FONT></TD>";
		else
			vCode = vCode + "<TD class=\"Caption\" WIDTH='14%'><CENTER><FONT COLOR='gray'>" + m + "</CENTER></FONT></TD>";
	}
	
	return vCode;
}

Calendar.prototype.format_day = function(vday) {
	var vNowDay = gNow.getDate();
	var vNowMonth = gNow.getMonth();
	var vNowYear = gNow.getFullYear();

	if (vday == vNowDay && this.gMonth == vNowMonth && this.gYear == vNowYear)
		return ("<FONT COLOR=\"RED\"><B>" + vday + "</B></FONT>");
	else
		return (vday);
}

Calendar.prototype.write_weekend_string = function(vday) {
	var i;
	// Return special formatting for the weekend day.
	for (i=0; i<weekend.length; i++) {
		if (vday == weekend[i])
			return (" BGCOLOR=\"" + weekendColor + "\"");
	}	
	return "";
}

Calendar.prototype.format_data = function(p_day) {
	var vData;
	var vMonth = 1 + this.gMonth;
	vMonth = (vMonth.toString().length < 2) ? "0" + vMonth : vMonth;
	var vMon = Calendar.get_month(this.gMonth).substr(0,3).toUpperCase();
	var vFMon = Calendar.get_month(this.gMonth).toUpperCase();
	var vY4 = new String(this.gYear);
	var vY2 = new String(this.gYear).substr(2,2);
	var vDD = (p_day.toString().length < 2) ? "0" + p_day : p_day;

	switch (this.gFormat) {
		case "MM\/DD\/YYYY" :
			vData = vMonth + "\/" + vDD + "\/" + vY4;
			break;
		case "MM\/DD\/YY" :
			vData = vMonth + "\/" + vDD + "\/" + vY2;
			break;
		case "MM-DD-YYYY" :
			vData = vMonth + "-" + vDD + "-" + vY4;
			break;
		case "MM-DD-YY" :
			vData = vMonth + "-" + vDD + "-" + vY2;
			break;
		case "MM.DD.YYYY" :
			vData = vMonth + "." + vDD + "." + vY4;
			break;
		case "MM.DD.YY" :
			vData = vMonth + "." + vDD + "." + vY2;
			break;

		case "DD\/MON\/YYYY" :
			vData = vDD + "\/" + vMon + "\/" + vY4;
			break;
		case "DD\/MON\/YY" :
			vData = vDD + "\/" + vMon + "\/" + vY2;
			break;
		case "DD-MON-YYYY" :
			vData = vDD + "-" + vMon + "-" + vY4;
			break;
		case "DD-MON-YY" :
			vData = vDD + "-" + vMon + "-" + vY2;
			break;
		case "DD.MON.YYYY" :
			vData = vDD + "." + vMon + "." + vY4;
			break;
		case "DD.MON.YY" :
			vData = vDD + "." + vMon + "." + vY2;
			break;

		case "DD\/MONTH\/YYYY" :
			vData = vDD + "\/" + vFMon + "\/" + vY4;
			break;
		case "DD\/MONTH\/YY" :
			vData = vDD + "\/" + vFMon + "\/" + vY2;
			break;
		case "DD-MONTH-YYYY" :
			vData = vDD + "-" + vFMon + "-" + vY4;
			break;
		case "DD-MONTH-YY" :
			vData = vDD + "-" + vFMon + "-" + vY2;
			break;
		case "DD.MONTH.YYYY" :
			vData = vDD + "." + vFMon + "." + vY4;
			break;
		case "DD.MONTH.YY" :
			vData = vDD + "." + vFMon + "." + vY2;
			break;

		case "DD\/MM\/YYYY" :
			vData = vDD + "\/" + vMonth + "\/" + vY4;
			break;
		case "DD\/MM\/YY" :
			vData = vDD + "\/" + vMonth + "\/" + vY2;
			break;
		case "DD-MM-YYYY" :
			vData = vDD + "-" + vMonth + "-" + vY4;
			break;
		case "DD-MM-YY" :
			vData = vDD + "-" + vMonth + "-" + vY2;
			break;
		case "DD.MM.YYYY" :
			vData = vDD + "." + vMonth + "." + vY4;
			break;
		case "DD.MM.YY" :
			vData = vDD + "." + vMonth + "." + vY2;
			break;

		case "YYYY\/MM\/DD" :
			vData = vY4 + "\/" + vMonth + "\/" + vDD;
			break;
		case "YY\/MM\/DD" :
			vData = vY2 + "\/" + vMonth + "\/" + vDD;
			break;
		case "YYYY-MM-DD" :
			vData = vY4 + "-" + vMonth + "-" + vDD;
			break;
		case "YY-MM-DD" :
			vData = vY2 + "-" + vMonth + "-" + vDD;
			break;
		case "YYYY.MM.DD" :
			vData = vY4 + "." + vMonth + "." + vDD;
			break;
		case "YY.MM.DD" :
			vData = vY2 + "." + vMonth + "." + vDD;
			break;

		case "YYYY\/DD\/MM" :
			vData = vY4 + "\/" + vDD + "\/" + vMonth;
			break;
		case "YY\/DD\/MM" :
			vData = vY2 + "\/" + vDD + "\/" + vMonth;
			break;
		case "YYYY-DD-MM" :
			vData = vY4 + "-" + vDD + "-" + vMonth;
			break;
		case "YY-DD-MM" :
			vData = vY2 + "-" + vDD + "-" + vMonth;
			break;
		case "YYYY.DD.MM" :
			vData = vY4 + "." + vDD + "." + vMonth;
			break;
		case "YY.DD.MM" :
			vData = vY2 + "." + vDD + "." + vMonth;
			break;

		default :
			vData = vMonth + "\/" + vDD + "\/" + vY4;
	}

	return vData;
}

function Build(p_item, p_format, p_date) {
	var p_WinCal = ggWinCal;
	gCal = new Calendar(p_item, p_WinCal, p_format, p_date);

	// Customize your Calendar here..
	gCal.gBGColor="white";
	gCal.gLinkColor="black";
	gCal.gTextColor="black";
	gCal.gHeaderColor="darkgreen";

	// Choose appropriate show function
	if (gCal.gYearly)	gCal.showY();
	else	gCal.show();
}

function show_calendar() {
	/* 
		p_item	: Return Item.
		p_format: Date format (mm/dd/yyyy, dd/mm/yy, ...)
		p_currdate: entered date
	*/
	var p_item, p_format, p_currdate;

	p_item = arguments[0];

	if (arguments[1] == null) p_format = "MM/DD/YYYY";
	else p_format = arguments[1];

	if (arguments[2] == "" || arguments[2] == null)
		p_currdate = new String(gNow);
	else
		p_currdate = arguments[2];
		
	vWinCal = window.open("", "Calendar", "width=206,height=200,status=no,resizable=no,top=200,left=200");
	vWinCal.opener = self;
	ggWinCal = vWinCal;

	Build(p_item, p_format, p_currdate);
}
/*
Yearly Calendar Code Starts here
*/
function show_yearly_calendar(p_item, p_format, p_date) {
	// Load the defaults..
	
	if (p_format == null || p_format == "")
		p_format = "MM/DD/YYYY";
    
    var vWinCal = window.open("", "Calendar", "scrollbars=yes,status=no,resizable=no");
	vWinCal.opener = self;
	ggWinCal = vWinCal;

	Build(p_item, p_format, p_date);
}

function checkdate(e_val, required)
{ 
	if (!required && e_val=="") {return true;}
	var err_date =  "Date not valid. Please enter as MM/DD/YYYY !";
	separ="/";
	k_sep=e_val.indexOf(separ);
	if (k_sep<0) return false;

	f_in = e_val.indexOf(separ);
	var v_month = e_val.substring(0,f_in);
	if (v_month.charAt(0)=="0")
		{v_month = v_month.substring(1,v_month.length);}
	var v_day = e_val.substring(f_in+1,e_val.indexOf(separ, f_in+1));
	if (v_day.charAt(0)=="0")
		{v_day = v_day.substring(1,v_day.length);}
	var v_year = e_val.substring(e_val.indexOf(separ,f_in+1)+1,e_val.length);
 
	var bcharscorrect = (isfound(v_day,digit) && isfound(v_month,digit) && isfound(v_year,digit) )
 
	if ((v_year.length !=4) || (!bcharscorrect)) return false;
	if (parseInt(v_year) <  1000) return false;

	if (!isNaN(parseInt(v_month)) && !isNaN(parseInt(v_day)) && !isNaN(parseInt(v_year)))
		{var Tdays = new Array(31,28,31,30,31,30,31,31,30,31,30,31);
	if ( Math.floor(parseInt(v_year)/4) == (parseInt(v_year)/4) )
		{Tdays[1] = 29;}
	if ( (parseInt(v_month)<=0) || (parseInt(v_day)<=0) || (parseInt(v_year) <= 0) || (parseInt(v_month)>12) || (parseInt(v_day)>Tdays[parseInt(v_month)-1]))
		return false;
  } else return false;
  
 return true;
}

function isfound(tstr, marr){ 
  var issfound = true;
  var i = 0;
  var k = 0;
  var tchr = " ";
  var foundchr = false;
  for ( i = 0; i < tstr.length; i++) {
    tchr = tstr.charAt(i);
    foundchr = false;
    for (k = 0; k < marr.length; k++)
	    if (tchr == marr.charAt(k)) foundchr = true;

    if (!foundchr) issfound = false;
  }
  return issfound;
}

function showdatepicker(dateitem, num_frm, required, date_format) { 

	var obj = document.forms[num_frm].elements[dateitem];	

	var e_val = FormatDateToJSStandard(obj.value, date_format);
	if (checkdate(e_val, required)) {
		if (e_val == "") eval("show_calendar('"+document.forms[num_frm].name+"."+dateitem+"','" + date_format+"');");
		else eval("show_calendar('"+document.forms[num_frm].name+"."+dateitem+"','"+date_format+"','" + e_val + "');");
	} else {
		obj.focus();
		alert("Date not valid. Please enter as MM/DD/YYYY !");
		eval("show_calendar('"+document.forms[num_frm].name+"."+dateitem+"');");	
	}
}

function FormatDateToJSStandard(p_date, local_format) {

	var vDate = new String(p_date);	
	if (vDate.length != 0) {
		
		var separ;
		var first_char = local_format.charAt(0);
		if (((first_char == "D") || (first_char == "M")) && (local_format.charAt(3) != "Y"))
			separ = new String(local_format).substr(2,1);
		else
			separ = new String(local_format).substr(4,1);
			
		var s_1 = vDate.indexOf(separ);
		var s_2 = vDate.indexOf(separ, s_1+1);
		
		var fD = vDate.substr(0, s_1);
		var sD = vDate.substr(s_1+1, s_2-s_1-1);
		var lD = vDate.substr(s_2+1);
		
		switch (local_format.charAt(0)) {
			case "D":
				switch (local_format.charAt(s_1+1)) {
					case "M":
						vDate = (sD.length==2?sD:"0"+sD) + "\/" + (fD.length==2?fD:"0"+fD) + "\/" + (lD.length==4?lD:"20"+lD);
						break;
					case "Y":
						vDate = (lD.length==2?fD:"0"+fD) + "\/" + (fD.length==2?fD:"0"+fD) + "\/" + (sD.length==4?lD:"20"+lD);
						break;
				}
				break;
			case "M":
				switch (local_format.charAt(s_1+1)) {
					case "D":
						vDate = (fD.length==2?fD:"0"+fD) + "\/" + (sD.length==2?sD:"0"+sD) + "\/" + (lD.length==4?lD:"20"+lD);
						break;
					case "Y":
						vDate = (fD.length==2?fD:"0"+fD) + "\/" + (lD.length==2?lD:"0"+lD) + "\/" + (sD.length==4?sD:"20"+sD);
						break;
				}
				break;
			case "Y":
				switch (local_format.charAt(s_1+1)) {
					case "D":
						vDate = (lD.length==2?lD:"0"+lD) + "\/" + (sD.length==2?sD:"0"+sD) + "\/" + (fD.length==4?fD:"20"+fD);
						break;
					case "M":
						vDate = (sD.length==2?sD:"0"+sD) + "\/" + (lD.length==2?lD:"0"+lD) + "\/" + (fD.length==4?fD:"20"+fD);
						break;
				}
				break;
		}
	}
	return vDate;
}
