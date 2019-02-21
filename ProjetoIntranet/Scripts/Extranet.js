$(document).ready(function () {

    
    $("#cabecalho").css("background-color", "rgba(0, 0, 0, 0.8)");

    var larguraTela = $(window).width();

    var y = 0;

    y = $(document).scrollTop();

    $(".carousel").carousel({ interval: 4000 });
    $(".carousel").carousel();

    
    $("#pt-Br").click(function () {
        debugger;
        $(this).parents("form").submit(); // post form
    });

    $("#en-us").click(function () {
        $(this).parents("form").submit(); // post form
    });
 
    //if (larguraTela > 990) {



    //    $(window).scroll(function () {

    //        y = $(document).scrollTop();

           

    //        if (y > 100) {

               

    //        } else {

    //            $("#cabecalho").css("background-color", "rgba(0, 0, 0, 0.8)");


    //        }



    //    });


    //}



    var t = 0;

    $(window).scroll(function () {

        t = $(document).scrollTop();


        if (t > 50) {

            document.getElementById("top-btn").style.display = "block";

        } else {

            document.getElementById("top-btn").style.display = "none";


        }



    });



    $("#top-btn").click(function () {

        $('html, body').animate({ scrollTop: 0 }, 'slow');

    });


    $("#hist").click(function () {


        $('html, body').scrollTo(500,1200, {axis:'y'});

    });

    $("#proc").click(function () {


        $('html, body').scrollTo(1100, 1200, { axis: 'y' });

    });


    $("#tecn").click(function () {


        $('html, body').scrollTo(1800, 1200, { axis: 'y' });

    });


    $("#qual").click(function () {


        $('html, body').scrollTo(2400, 1200, { axis: 'y' });

    });

    $("#loca").click(function () {


        $('html, body').scrollTo(2900, 1200, { axis: 'y' });

    });


    $("#cont").click(function () {


        $('html, body').scrollTo(3500, 1200, { axis: 'y' });

    });

    $("#home").click(function () {


        $('html, body').scrollTo(0, 1200, { axis: 'y' });

    });


    // ------------------------------- Alto contraste --------------------------------------------------- //


    //classes de layout
    jQuery('div.layout').addClass('layout_classes');


    //acao botao de alto contraste
    jQuery('a.toggle-contraste').click(function () {
        debugger;
        if (!jQuery('div.layout').hasClass('contraste')) {
            jQuery('div.layout').addClass('contraste');
            
            layout_classes = ('layout_classes');
            if (layout_classes != 'undefined')
                layout_classes = layout_classes + ' contraste';
            else
                layout_classes = 'contraste';
            
        }
        else {
            jQuery('div.layout').removeClass('contraste');           
            layout_classes = ('layout_classes');
            layout_classes = layout_classes.replace('contraste', '');
            
        }
    });
	//fim acao botao de alto contraste
           
    

         

   
   
    

});



