$(document).ready(function () {
    // variaveis referente a view EditarMenu-------

    var listaDeLInks;
    var idPai = 0;
    var arrayLinksALterados = [];
    var arrayFiltrado = []; //cria vetor dinamico
    var objPai;
   
    $("#myModal").modal({ show: false });
    $("#myModal2").modal({ show: false });
    $("#myModal3").modal({ show: false });
    $("#myModal4").modal({ show: false });
    $("#feedBackModalURL").modal({ show: false });
    $("#feedBackModalVazio").modal({ show: false });
    $("#feedBackModalLinkExcluido").modal({ show: false });
    $("#feedbackCriarGrupo").modal({ show: false });
    $("#feedbackExcluirGrupo").modal({ show: false });
    $("#feedbackCamposVazios").modal({ show: false });
    $("#postModal").modal({ show: false });
    //----------------------------------------------
    
    

    // funções referentes a view EditarMenu------------------------

    function listaFilhos() {



        var tabela = document.getElementById("tabela");

        var thead = document.createElement("thead");

        tabela.appendChild(thead);

        var row = '';


        var calangos = tabela.children.length;

        var x = 0;

        arrayFiltrado = [];
        debugger;

        $("#tbody_editar").empty();

        while (x < listaDeLInks.length) {

            var obj = listaDeLInks[x];

            if (obj.id == idPai) {
                x++;
                objPai = obj;
                continue;
            }
            arrayFiltrado.push(obj);

            row += '<tr>';

            row += '<td>' + obj.id + '</td>';
            row += '<td>' + obj.url + '</td>';
            row += '<td>' + obj.nome + '</td>';
            row += '<td>' + obj.ordem + '</td>';
            row += '<td>' + obj.codigoPai + '</td>';
            row += '<td>' + '<input type="checkbox" id="' + obj.id + '"' + ' name="chk_ids" class="check" value="' + obj.id + '"' + '>' + '</td>';


            row += '</tr>';




            x++;
        }


        $("#tabela").append(row);

    }


    $(".botao").click(function () {




        $("#tbody_editar").empty();


        idPai = this.id;

        $.getJSON('/Menu/JsonLinksFilhos', function (data) {



            listaDeLInks = data;

            document.getElementById('idPai').innerHTML = idPai;




            listaFilhos();


        });





    });
  

    $("#btn_confirm").click(function () {

        

        var links_checados = $("input[name='chk_ids']:checked").length;

        if (links_checados > 0) {

            var arr = document.getElementsByClassName('check');





            for (z = 0; z < arr.length; z++) {




                if (arr[z].checked) {



                    var objeto = arrayFiltrado[z];

                    objeto.codigoPai = idPai;


                    arrayLinksALterados.push(objeto);

                }

            }

            $("#myModal").modal('show');

        } else {

            $("#myModal2").modal('show');

        }

        

    });


    $("#btn_cancelar_associacao").click(function () {


        $(".check").removeAttr('checked');

        


    });

    $("#botao_enviar").click(function () {

        if (arrayLinksALterados === null || arrayLinksALterados.length == 0) {

            $("#myModal4").modal('show');


        } else {

            var dados = JSON.stringify(arrayLinksALterados);



            $.ajax({

                url: '/Menu/EditarMenu',
                type: 'POST',


                data: "dados=" + dados, // atenção aqui posi deve ser aspas duplas e conter o mesmop  nome nos parematros da action

                success: function () {

                    sucesso();
                },
                error: function () {

                    alert('Erro ao salvar!!');
                }

            })

        }
        
        

    });

    function sucesso(){


        $("#myModal3").modal('show');

        arrayLinksALterados = [];
        arrayFiltrado = []

        setTimeout(function () {
            location.reload();
        }, 2000);

        
    }

    //--------------------------------------------------------------


    //-------------------------funções referentes a view adicionar links-------------

    

    $("#btn_salvar_url").click(function () {

        debugger;   

        var valor_url = $("#url").val();
        var nomeURL_txb = $("#nome").val();

        if (valor_url != "" && nomeURL_txb != "" ) {

            var objLink = { nome: nomeURL_txb, url: valor_url};
            

            $.ajax({

                url: '/Menu/AdicionarLinks',
                type: 'POST',


                data: "link=" + JSON.stringify(objLink),

                success: function () {

                    $("#feedBackModalURL").modal('show');

                    setTimeout(function () {
                        location.reload();
                    }, 2000);

                },
                error: function () {

                    alert('Erro ao salvar!!');
                }

            })

            
            $("#url").val("");
            $("#nome").val("");
           

        } else {


            $("#feedBackModalVazio").modal('show');

        }

        

    });

    

    


    //------------------------------------------------------------------------------


    //------------------------Funções referentes a Editar na Lista de Links----------------------

    

    $(".btnExcluirLink2").on('click', function () {

        var idlink = this.id;
        debugger;
        $.ajax({

            url: '/Menu/Excluir/',
            type: 'POST',


            data: "id=" + idlink, // o id string deve ser igual ao parametro da acttion no asp, ou seja mesmo nome

            success: function () {

                $("#feedBackModalLinkExcluido").modal('show');

                setTimeout(function () {
                    location.reload();
                }, 2000);

            },
            error: function () {

                alert('Erro ao salvar!!');
            }

        })


       

        

    });

    


    //-----------------------------------------------------------------------------



    //------------------------ funções referentes a view criar grupo ----------------

    $("#btn_SalvarGrupo").click(function () {

        

        var nomeGrupo = $(".nomeGrupo").val();
        var descGrupo = $(".descricaoGrupo").val();
        var urlGrupo = $("#graficoUrl").val();

       
        var objPrivilegios = "";

        var array_privilegios = document.getElementsByClassName('privilegios');

       var prvivilegiosChecados = $("input[name='privilegios']:checked").length;

        

        if (nomeGrupo != "" && prvivilegiosChecados > 0) {


            for (var indice = 0; indice < array_privilegios.length; indice++) {


                if (array_privilegios[indice].checked) {

                    objPrivilegios += "," + array_privilegios[indice].value;

                }

            }
            var objGrupo = { nome: nomeGrupo, descricao: descGrupo, graficoUrl: urlGrupo, privilegios: objPrivilegios };

            var obj1 = JSON.stringify(objGrupo);




            $.ajax({

                url: '/GrupoUsuario/CriarGrupo/',
                type: 'POST',



                data: "json=" + obj1,

                success: function () {



                    $("#feedbackCriarGrupo").modal('show');


                },
                error: function () {

                    alert('Erro ao salvar!!');
                }

            })

            $(".nomeGrupo").val("");
            $(".descricaoGrupo").val("");
            $("#graficoUrl").val("");
            $(".privilegios").removeAttr('checked');

        } else {

            $("#feedbackCamposVazios").modal('show');

        }
        
                

    });
   

    //--------------------------------------------------------------------------------



    //-----------------------------------funções referentes a view lista de grupos-------


    $(".btnExcluirGrupos").on('click', function () {

        var idGrupo = this.id;
        
        $.ajax({

            url: '/GrupoUsuario/Excluir/',
            type: 'POST',


            data: "id=" + idGrupo, 

            success: function (response) {

                if (response.success && response != null) {

                    
                    $("#feedbackExcluirGrupo").modal('show');

                    setTimeout(function () {
                        location.reload();
                    }, 2000);

                } else {

                    $("#feedbackExcluirGrupoFalha").modal('show');

                   

                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                }

                

            },
            error: function () {

                alert('Erro ao salvar!!');
            }

        })






    });


    //--------------------------------------------------------------------------------


    //--------------------------Funções dos posts it------------------------------


    var listaPosts = [];
   
    $.getJSON('/Postagens/ListarPostagensJson', function (data) {

        debugger;
  
        listaPosts = data;



        for (var index = 0; index < listaPosts.length; index++) {

            var content = document.getElementById("post");
            var obj = listaPosts[index];


            var divPost = document.createElement("div");
            divPost.className = "post-it";
            divPost.id = obj.id;   

            var hr = document.createElement("hr");


            var divTitulo = document.createElement("div");
            divTitulo.innerHTML = obj.titulo;

            var divRow = document.createElement("div");
            divRow.className = "Row";


            var textArea = document.createElement("textarea");
            textArea.cols = 120;
            textArea.className = "textPost"

            textArea.innerHTML = obj.corpo;

            divRow.appendChild(textArea);

            divPost.appendChild(divTitulo);

            divPost.appendChild(hr);

            divPost.appendChild(divRow);
                       
                    

            content.appendChild(divPost);
            
        }

        
        var dialog = document.getElementById("dialog");
        var p = document.createElement("p");

        $(".post-it").on("click", function () { //evento deve ficar dentro da função para não ser adcionado antes sem qu exita elemntos

            var postIdClicado = this.id;
            
           

            $(".parag").empty();
            $(".parag").toggleClass("desativo");

                for (var j = 0; j < listaPosts.length; j++) {


                    var obj = listaPosts[j];

                    

                    if (obj.id == postIdClicado) {

                        


                        
                        p.className = "parag";
                        p.innerHTML = obj.corpo;


                        dialog.appendChild(p);

                        $(".parag").toggleClass("ativo");
                        

                        break;

                    } 

                    if ((!p.hasChildNodes()) && (p.className == "desativo")) {

                        dialog.removeChild(".desativo");
                    }
                    
                }

                
                
                $("#postModal").modal('show');
            
                
        });

        
        

    });

    $("#titulo").val("");
    $("#corpo").val("");
    $("#etiqueta").val("");

    $("#btn-enviar-postagem").click(function () {
    




        if ($("#titulo").val() == "" || $("#corpo").val() == "" || $("#etiqueta").val() == "") {

            $("#postagemVazia").modal('show');

        } 

        
        
       
    });

    

    
    //--------------------------------------------------------------------------------

   
    //-----------------------funções referentes a view listar cucrriculos-------------

    var objCurriculo;
    var listaDeCurriculos;

    $(".detalhar").click(function () {
        
        detalheId = this.id;
      
        $.getJSON('/Curriculo/Detalhes', function (data) {

            
             listaDeCurriculos = data;

             for (var ind = 0; ind < listaDeCurriculos.length; ind++) {


                 if (listaDeCurriculos[ind].id == detalheId) {

                     objCurriculo = listaDeCurriculos[ind];

                     break;

                 }


             }


             setTimeout(function () {
                 renderizar();
             }, 1000);




        });

       
       


        

    });


    function renderizar() {

        var txbNome = document.getElementById("txbNome");
        txbNome.setAttribute("value", objCurriculo.nome);

        var txbSexo = document.getElementById("txbSexo");


        if (objCurriculo.genero == 1) {

            txbSexo.setAttribute("value", "Masculino");
        } else {

            txbSexo.setAttribute("value", "Feminino");
        }

        var txbEstadoCivil = document.getElementById("txbEstadoCivil");
        txbEstadoCivil.setAttribute("value", objCurriculo.estado.estado)



        var txbNasc = document.getElementById("txbNasc");
        txbNasc.setAttribute("value", objCurriculo.dataNascimento) // bugado verificar depois




        var txbTel = document.getElementById("txbTel");
        txbTel.setAttribute("value", objCurriculo.telefoneFixo);

        var txbCel = document.getElementById("txbCel");
        txbCel.setAttribute("value", objCurriculo.telefoneCelular);

        var txbEmail = document.getElementById("txbEmail");
        txbEmail.setAttribute("value", objCurriculo.email);

        var txbEmail = document.getElementById("txbEmail");
        txbEmail.setAttribute("value", objCurriculo.email);

        var txbSite = document.getElementById("txbSite");
        txbSite.setAttribute("value", objCurriculo.siteBlog);

        var txbSkype = document.getElementById("txbSite");
        txbSkype.setAttribute("value", objCurriculo.skype);

        var txbArea = document.getElementById("txbArea");
        txbArea.setAttribute("value", objCurriculo.area.cargo);

        var txbRemuneracao = document.getElementById("txbRemuneracao");
        txbRemuneracao.setAttribute("value", objCurriculo.remuneracao);

        var txbCpf = document.getElementById("txbCpf");
        txbCpf.setAttribute("value", objCurriculo.cpf);

        var txbCep = document.getElementById("txbCep");
        txbCep.setAttribute("value", objCurriculo.cep);

        var txbRua = document.getElementById("txbRua");
        txbRua.setAttribute("value", objCurriculo.rua);

        var txbNum = document.getElementById("txbNum");
        txbNum.setAttribute("value", objCurriculo.numero);


        var txbBairro = document.getElementById("txbBairro");
        txbBairro.setAttribute("value", objCurriculo.bairro);

        var txbCidade = document.getElementById("txbCidade");
        txbCidade.setAttribute("value", objCurriculo.cidade);


        var txbUf = document.getElementById("txbUf");
        txbUf.setAttribute("value", objCurriculo.uf);


        var txbDesc = document.getElementById("txbDesc");
        txbDesc.setAttribute("value", objCurriculo.descricao);

        $("#detalhes").modal('show');

    }

    //----------------------------fim view listar curriculos ---------------------------



    //------------------------- funções referentes a view desativar usuario----------



    $(".desativar").click(function () {

        debugger;
        var usrId = this.id;

        $.ajax({

            url: '/Usuario/DesativarUsuario/',
            type: 'POST',


            data: "id=" + usrId,

            success: function () {

               // $("#feedbackExcluirGrupo").modal('show');

                $("#feedbackDesativado").modal('show');

                setTimeout(function () {
                    location.reload();
                }, 2000);

            },
            error: function () {

                alert('Erro ao salvar!!');
            }

        })


    });


    $(".ativar").click(function () {

        
        var usrId = this.id;

        $.ajax({

            url: '/Usuario/ativarUsuario/',
            type: 'POST',


            data: "id=" + usrId,

            success: function () {

                // $("#feedbackExcluirGrupo").modal('show');

                $("#feedbackAtivado").modal('show');

                setTimeout(function () {
                    location.reload();
                }, 2000);

            },
            error: function () {

                alert('Erro ao salvar!!');
            }

        })


    });




    //-------------------------------fim desativar usuario ------------------------------------------


   
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
     

    // ------------------------------- Fim Alto contraste --------------------------------------------------- //

    //----------------------------- funções referentes a  view Associação de LInks e Grupos----------------------------


    $("#btn_salvar_Associacao").click(function () {


        var links = document.getElementsByClassName("checkLinks");



        var idGrupo = $("#ListaGruposAssociar").val();

        var listaDeIdsDoLinks = [];

        for (var k = 0; k < links.length; k++) {


            if (links[k].checked) {

                listaDeIdsDoLinks.push(links[k].value);

            }


        }
        if (listaDeIdsDoLinks.length == 0 || listaDeIdsDoLinks == null) {

            $("#feedbackAssociarGrupoErro").modal('show');

        } else {

            var objIDs = { GrupoId: idGrupo, LinksIds: listaDeIdsDoLinks };

            $.ajax({




                url: '/Menu/AssociarLinksEGrupos/',
                type: 'POST',


                data: "associarGrupos=" + JSON.stringify(objIDs),

                success: function () {

                  

                    $("#feedbackAssociarGrupoLink").modal('show');

                    setTimeout(function () {
                        location.reload();
                    }, 2000);

                },
                error: function () {

                    alert('Erro ao salvar!!');
                }

            })


        }




    });




    // --------------------------------fim funções referentes a  view Associação de LInks e Grupos --------------------------------------



    //----------------------------- funções referentes a  view Desassociação de LInks e Grupos----------------------------


    $("#ListaGruposDesassociar").on('change', function () {



        var listaDeLInksFiltrados = [];



        var idGrupoSelecionado = $("#ListaGruposDesassociar").val();





        $.ajax({




            url: '/Menu/ListarLinksAssociacao/',
            type: 'POST',

            async: true,


            data: "GrupoSelecionado=" + idGrupoSelecionado,

            success: function (data, status, XMLHttpRequest) {



                listaDeLInksFiltrados = data;

                $("#tbCorpo").empty();

                var tb_links = document.getElementById("tb_links");

                var thead = document.createElement("thead");


                var tbody = document.createElement("tbody");

                tb_links.appendChild(thead);

                var row = '';




                for (var index = 0; index < listaDeLInksFiltrados.length; index++) {

                    row += '<tr>';
                    row += '<td>' + listaDeLInksFiltrados[index].id + '</td>';
                    row += '<td>' + listaDeLInksFiltrados[index].url + '</td>';
                    row += '<td>' + listaDeLInksFiltrados[index].nome + '</td>';
                    row += '<td>' + '<button type="button" id="' + listaDeLInksFiltrados[index].id + '"' + ' class=" btn btn-danger glyphicon glyphicon-trash btn-excluir-associacao">' + '</button>' + '</td>';
                    row += '<tr>';

                }



                $("#tb_links").append(row);

                createEvent(); //necessário para criar o evento e anexar ele ao elemento depois de renderizado

            },
            error: function () {

                alert('Erro ao enviar requisição');
            }

        })






    });

    function createEvent() {
        $(".btn-excluir-associacao").click(function () {

            debugger;
            var linksIds = [];

            linksIds[0] = this.id;

            var GrupoId = $("#ListaGruposDesassociar").val();

            var objId = { GrupoId: GrupoId, linksIds: linksIds };



            debugger;
            $.ajax({




                url: '/Menu/ExcluirAssociacao/',
                type: 'POST',

                async: true,


                data: "ids=" + JSON.stringify(objId),

                success: function () {

                    $("#feedbackDesaAssociarGrupoLink").modal('show');

                    setTimeout(function () {
                        location.reload();
                    }, 1000);

                },
                error: function () {

                    alert('Erro ao enviar requisição');
                }

            })


        });
    }



    //----------------------------- Fim das funções referentes a  view Associação de LInks e Grupos----------------------------


    //-----------------------------  funções referentes a  view  listar Postagens ----------------------------


    $(".btnExcluirPostagem").click(function () {

        debugger;
        var post_id;

        post_id = this.id;

        

        debugger;
        $.ajax({




            url: '/Postagens/Excluir/',
            type: 'POST',

            async: true,


            data: "id=" + post_id,

            success: function () {

                $("#feedbackExcluirPostagem").modal('show');

               

                setTimeout(function () {
                    location.reload();
                }, 1000);

            },
            error: function () {

                alert('Erro ao enviar requisição');
            }

        })


    });
   




     //----------------------------- fim das funções referentes a  view  listar Postagens ----------------------------

    




});

