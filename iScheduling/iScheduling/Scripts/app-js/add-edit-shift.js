$(document).ready(() => {

    initModal = () => {

        var dateOfShiftPicker = $('#dateOfShiftPicker');

        // This is employee view as DateOfShiftPicker only available in Employee View
        if (dateOfShiftPicker != undefined && dateOfShiftPicker != null) {

            var startDate = $('#startDate').val();
            var endDate = $('#endDate').val();

            var dateOfShift = $('#dateOfShift').val();

            var initialDate = new Date(startDate);

            if (dateOfShift != '' & dateOfShift != null && dateOfShift != undefined)
                initialDate = new Date(dateOfShift);


            dateOfShiftPicker.datepicker({
                startDate: startDate,
                endDate: endDate,
                autoclose: true,
                format: "MM-dd-yyyy"
            }).datepicker("setDate", initialDate);
        }
    }

    $('#start-time-picker .cell').on('click', function () {
        $('#start-time-picker .cell').removeClass('select');
        $(this).addClass('select');

        $('#start-time-picker').removeClass('show-time-picker');
        $('#start-time-picker').addClass('hide-time-picker');


        var selectedTime = $(this).text();

        $('#start-time').val(selectedTime);
    });

    $('#end-time-picker .cell').on('click', function () {
        $('#end-time-picker .cell').removeClass('select');
        $(this).addClass('select');

        $('#end-time-picker').removeClass('show-time-picker');
        $('#end-time-picker').addClass('hide-time-picker');


        var selectedTime = $(this).text();

        $('#end-time').val(selectedTime);
    });

    chooseStartTime = () => {
        $('#start-time-picker').removeClass('hide-time-picker');
        $('#start-time-picker').addClass('show-time-picker');
    }

    chooseEndTime = () => {
        $('#end-time-picker').removeClass('hide-time-picker');
        $('#end-time-picker').addClass('show-time-picker');
    }

    submitAddOrUpdateShift = () => {
        var isCalendarView = $('#isCalendarView').val();
        var shiftId = $('#shiftId').val();
        var employeeId = '';
        var dateOfShift = '';
        if (isCalendarView == 'true') {
            employeeId = $('#ddlEmployeeId').val();
            dateOfShift = $('#dateOfShift').val();
        }
        else {
            employeeId = $('#employeeId').val();
            dateOfShift = $('#dateOfShiftPicker').val();
        }

        var startTime = $('#start-time').val();
        var endTime = $('#end-time').val();


        var action = (shiftId == '' || shiftId == undefined || shiftId == null) ? 'Add' : 'Edit';
        var url = `/Shift/${action}`;

        var data = {
            IsCalendarView: isCalendarView,
            ShiftId: shiftId,
            AssignedShiftEmployeeId: employeeId,
            DateOfShift: dateOfShift,
            ShiftStartAt: startTime,
            ShiftEndAt: endTime
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list shifts page
            var selectedEndDate = $('#shiftEndDatePicker').val();
            $('#shiftEndDatePicker').datepicker("setDate", selectedEndDate);

            $('#add-edit-shift').modal('hide');
        });
    }

    initModal();
});