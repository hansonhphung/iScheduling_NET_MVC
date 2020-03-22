$(document).ready(() => {

    init = () => {
        // Data Picker Initialization

        var initialDate = $('#selected-date').val();

        var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

        var shiftDate = today;

        if (initialDate != '') {
            shiftDate = new Date(initialDate);
        }
        
        $('#shiftdatePicker').datepicker({
            startDate: today,
            autoclose: true,
            format: "MM dd yyyy"
        }).on('changeDate', (e) => {
            var url = `/Shift/List`;

            var selectedDate = new Date(e.date).toUTCString();

            var data = { date: selectedDate };

            $.post(url, data, (res) => {
                $('#list-shifts').html(res);
            });

        }).datepicker("setDate", shiftDate);

    }

    editShift = (shiftId) => {
        var url = `/Shift/Edit?shiftId=${shiftId}`;

        $.get(url, (data) => {
            $('#add-shift-modal').html(data);

            $('#add-edit-shift').modal('show');
        });
    }

    getShiftInfo = () => {
        var selectedDate = $('#shiftdatePicker').val();

        var url = `/Shift/Add?selectedDate=${selectedDate}`;

        $.get(url, (data) => {
            $('#add-shift-modal').html(data);

            $('#add-edit-shift').modal('show');

            $('#start-time').val('');
            $('#end-time').val('');
        });
    }

    init();
});