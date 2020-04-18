$(document).ready(() => {

    formatDate = (date) => {
        var d = date.getDate();
        var m = date.getMonth() + 1;
        var y = date.getFullYear();

        return (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d) + '-' + y;
    }

    init = () => {
        // Data Picker Initialization

        var isCalendarView = $('#is-calendar-view').val();

        if (isCalendarView === "true") {
            var initialDate = $('#selected-date').val();

            var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

            var shiftDate = today;

            if (initialDate != undefined && initialDate != "" && initialDate != null) {
                shiftDate = new Date(initialDate);
            }

            $('#shiftdatePicker').datepicker({
                startDate: today,
                autoclose: true,
                format: "MM-dd-yyyy"
            }).on('changeDate', (e) => {
                var url = `/Shift/List`;

                var selectedDate = formatDate(new Date(e.date));
                
                var data = { date: selectedDate };

                $.post(url, data, (res) => {
                    $('#list-shifts').html(res);
                });

            }).datepicker("setDate", shiftDate);
        } else {
            var today = formatDate(new Date());

            // Automatically get the current week
            var today = new Date();
            var thisSunday = new Date(new Date().setDate(today.getDate() - today.getDay()));
            var thisSaturday = new Date(new Date().setDate(today.getDate() - today.getDay() + 6));

            thisSunday = formatDate(thisSunday);
            thisSaturday = formatDate(thisSaturday);

            $('#shiftStartDatePicker').datepicker({
                autoclose: true, format: "MM-dd-yyyy"
            }).on('changeDate', (e) => {
                var empId = $('#empId').val();

                var startDate = formatDate(new Date(e.date));
                var endDate = $('#shiftEndDatePicker').val();
                if (endDate == "" || endDate == null || endDate == undefined)
                    endDate = startDate;

                var url = `/Shift/${empId}/${startDate}/${endDate}`;

                $.get(url, (res) => {
                    $('#list-shifts-employee-view').html(res);
                });
            }).datepicker("setDate", thisSunday);

            $('#shiftEndDatePicker').datepicker({
                autoclose: true, format: "MM-dd-yyyy"
            }).on('changeDate', (e) => {
                var empId = $('#empId').val();

                var endDate = formatDate(new Date(e.date));
                var startDate = $('#shiftStartDatePicker').val();
                if (startDate == "" || startDate == null || startDate == undefined)
                    startDate = endDate;

                var url = `/Shift/${empId}/${startDate}/${endDate}`;

                $.get(url, (res) => {
                    $('#list-shifts-employee-view').html(res);
                });
            }).datepicker("setDate", thisSaturday);
        }
    }

    editShift = (shiftId, isCalendarView) => {
        var url = `/Shift/Edit?shiftId=${shiftId}&isCalendarView=${isCalendarView}`;

        $.get(url, (data) => {
            $('#add-shift-modal').html(data);

            $('#add-edit-shift').modal('show');
        });
    }

    addShift = (isCalendarView) => {
        var selectedDate = $('#shiftdatePicker').val();
        var empId = $('#empId').val();

        var url = '';

        if (isCalendarView)
            url = `/Shift/AddCalendarView?selectedDate=${selectedDate}`;
        else {
            var startDate = $('#shiftStartDatePicker').val();
            var endDate = $('#shiftEndDatePicker').val();
            url = `/Shift/AddShiftEmployeeView?employeeId=${empId}&startDate=${startDate}&endDate=${endDate}`
        }
            
        $.get(url, (data) => {
            $('#add-shift-modal').html(data);

            $('#add-edit-shift').modal('show');

            $('#start-time').val('');
            $('#end-time').val('');
        });
    }

    deleteShift = (shiftId) => {
        var isCalendarView = $('#isCalendarView').val();

        var url = `/Shift/Delete?shiftId=${shiftId}`;

        $.get(url, (deleteRes) => {

            if (isCalendarView == "true") {
                var callbackURL = `/Shift/List`;

                var shiftDate = $('#dateOfShift').val();

                var data = { date: shiftDate };

                $.post(callbackURL, data, (lstShiftRes) => {
                    $('#list-shifts').html(lstShiftRes);

                    $('#add-edit-shift').modal('hide');
                });
            } else {
                // reload list shifts page
                var selectedEndDate = $('#shiftEndDatePicker').val();
                $('#shiftEndDatePicker').datepicker("setDate", selectedEndDate);

                $('#add-edit-shift').modal('hide');
            }

            
        });
    }

    init();

});