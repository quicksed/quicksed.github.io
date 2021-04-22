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