var incomeChart = c3.generate({
    bindto: '#incomeChart',
    data: {
        columns: [
            ['Зарплата', 100000],
            ['Дивиденды', 5500],
            ['Подарок', 6500]
        ],
        type: 'pie'
    }
});

var consumptionChart = c3.generate({
    bindto: '#consumptionChart',
    data: {
        columns: [
            ['Ремонт машины', 10000],
            ['Еда', 25000],
            ['Свет, газ и вода', 6500],
            ['Обувь', 10000],
            ['Подарок', 85000],
        ],
        type: 'pie'
    }
});

$(function () {
    var start = moment().subtract(5, 'months');
    var end = moment();

    function cb(start, end) {
        $('#reportRange span').html(start.format('D:MM:YYYY') + ' - ' + end.format('D:MM:YYYY'));
    }

    $('#reportRange').daterangepicker({
        ranges: {
            'Сегодня': [moment(), moment()],
            'Вчера': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Последняя неделя': [moment().subtract(6, 'days'), moment()],
            'Последние 30 дней': [moment().subtract(29, 'days'), moment()],
            'Этот месяц': [moment().startOf('month'), moment().endOf('month')],
            'Предыдущий месяц': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);
});
