$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: "/Saida/LimparLista",
        success: function (result) {
            $("#listSaidasSelects").html(result);
        }
    });
    $.ajax({
        type: 'GET',
        url: "/Saida/ReturnList",
        success: function (result) {
            $("#listSaidasSelects").html(result);
        }
    });
    $('.add-ajax').click(function () {
        var producaoId = document.querySelector("select#producaoId");
        var qtSelecionada = document.querySelector("input#qtLancada");
        const id = producaoId.value;
        const qt = qtSelecionada.value;
        if (id == '' || qt == '') {
            window.alert("Todos os campos são obrigatórios!");
        }
        else if (qt <= 0) {
            window.alert("Quantidade inválida!");
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/Saida/AddSaidaTemp',
                data: {
                    id: id,
                    qt: qt
                },
                success: function (result) {
                    $("#listSaidasSelects").html(result);
                }
            });
        }
    });
});





