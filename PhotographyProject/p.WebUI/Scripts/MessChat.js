


var mainDiv = $(document.getElementById('conversationsDiv'));


var theChat;

function connectToChat(id) {
    theChat = new Chat();
    theChat.connect(id);
}

function Chat(type)
{
    var chat = $.connection.messMe;
    var connectId;

    this.connect = function (id) {


        chat.client.sendMessage = function (message) {
            var divId = '#conv-' + message.ConversationId;
            var div = mainDiv.find(divId);
            var links = $('#conversationsPlace').find('.conversationLinq');
            if ($(div).is(':hidden')) {
                $.each(links, function (i, link) {
                    if ($(link).data('id') == message.ConversationId) {
                        $("#newmessageSound")[0].play();
                        $(link).find('.conversationUserLink').css('background-color', '#b6ff00');
                    }
                });
            }
            var msg = div.find('.msg');
            appendMessage(message, msg);
        };

        chat.client.addNice = function (data) {
            var where = $('#workbenchContent');
            var div = window.Finder.findInDiv(where, 'worldContainer', 'picture', data.Id);
            if (div != null) {
                window.Templates.addNice(data.UserId, div);
            }
        }

        chat.client.addNot = function (data) {
            var where = $('#workbenchContent');
            var div = window.Finder.findInDiv(where, 'worldContainer', 'picture', data.Id);
            if (div != null) {
                window.Templates.addNot(data.UserId, div);
            }
        }

        chat.client.alertMessage = function (message) {
            alert(message);
        }
        chat.client.reconnect = function (id) {
            theChat.connectId = id;
        }

        chat.client.connectionId = function (id) {
            theChat.connectId = id;
        };

        $.connection.hub.start(function () {
            chat.server.connect(id);
        });
    }

    this.conId = function () {
        return theChat.connectId;
    }

    this.niceToAll = function (pictureId, userId) {
        chat.server.niceToAll(pictureId,userId);
    }

    this.notToAll = function (pictureId, userId) {
        chat.server.notToAll(pictureId, userId);
    }

    this.sendMessage = function (value, conversationId, partenerId, connectionId, myId, record,interval) {
        chat.server.sendToUser(value, conversationId, partenerId, connectionId, myId,record,interval);
    }
}

function submitMessage(form) {
    var input = $(form).find('.messageField');
    var value = input.val();
    if (value != '') {
        var partenerId = $(form).data('user');
        var conversationId = $(form).data('conversation');
        var connectionId = theChat.conId();
        var myId = $(form).data('myid');
        var record = $(form).data('record');
        var interval = $(form).data('inteval');
        theChat.sendMessage(value, conversationId, partenerId, connectionId, myId, record,interval);
        input.val('');
    }
}
function changeInterval(select) {
    var form = $(select).parent().parent().find('.textMessageForm');
    form.data('inteval', $(select).val());
}

function recording(button) {
    var form = $(button).parent().parent().find('.textMessageForm');
    var record = form.data('record');
    var select = $(button).parent().find('.selectLifeInterval');
    if (record == "false") {
        form.data('record', 'true');
        $(button).css('color', 'white');
        select.hide();
        $(form).data('inteval',0);
    } else {
        form.data('record', 'false');
        $(button).css('color', 'red');
        select.show();
    }
}





function activatte(img) {
    $(img).parent().find('input').trigger('click');
}


function startChat(targetDiv, id) {
    writeConversation(targetDiv, id);
}


var pipeLastId = 0;
function setPipeLastId(id) {
    if (pipeLastId < id) {
        pipeLastId = id;
    }
}

function fileUpload(form, action_url, div_id) {

    // Create the iframe...
    var iframe = document.createElement("iframe");
    iframe.setAttribute("id", "upload_iframe");
    iframe.setAttribute("name", "upload_iframe");
    iframe.setAttribute("width", "0");
    iframe.setAttribute("height", "0");
    iframe.setAttribute("border", "0");
    iframe.setAttribute("style", "width: 0; height: 0; border: none;");
    // Add to document...
    $(form).append(iframe);
    window.frames['upload_iframe'].name = "upload_iframe";
    iframeId = document.getElementById("upload_iframe");

    // Add event...
    var eventHandler = function () {

        if (iframeId.detachEvent) iframeId.detachEvent("onload", eventHandler);
        else iframeId.removeEventListener("load", eventHandler, false);

        if (iframeId.contentDocument) {
            content = iframeId.contentDocument.body.innerHTML;
        } else if (iframeId.contentWindow) {
            content = iframeId.contentWindow.document.body.innerHTML;
        } else if (iframeId.document) {
            content = iframeId.document.body.innerHTML;
        }

        document.getElementById(div_id).innerHTML = content;

        setTimeout('iframeId.parentNode.remove(iframeId)', 250);
    }
    if (iframeId.addEventListener) iframeId.addEventListener("load", eventHandler, true);
    if (iframeId.attachEvent) iframeId.attachEvent("onload", eventHandler);
    var dataForm = $(form).parent().find('.textMessageForm');
    var record = $(dataForm).data('record');
    var interval = $(dataForm).data('inteval');
    action_url += '&conid=' + theChat.conId() + '&record=' + record + '&interval=' + interval;
    $(form).attr("target", "upload_iframe");
    $(form).attr("action", action_url);
    $(form).attr("method", "post");
    $(form).attr("enctype", "multipart/form-data");
    $(form).attr("encoding", "multipart/form-data");
    form.submit();
    document.getElementById(div_id).innerHTML = "Uploading...";
}

function writeConversation(targetDiv, id) {
    
    var conv = $(document.createElement('div'));
    $(conv).attr('id', 'conv-' + id);
    $(conv).attr('class', 'conversationMessages');
    $(conv).data('id', id);
    $(conv).data('lastId', '');

    var conversationPartener = $(document.createElement('div'));
    $(conversationPartener).attr('class', 'conversationPartener');

    var hideButton = $(document.createElement('button'));
    hideButton.attr('class', 'hideConversation');
    hideButton.text('X');
    $(hideButton).data('id', id);
    $(hideButton).on('click', function () {
        hideConv(this,id);
    });
    conversationPartener.append(hideButton);

    loadPartener(conversationPartener, id);
    conv.append(conversationPartener);


    var msg = $(document.createElement('div'));
    msg.attr('class', 'msg');
    var lastId = loadConversationMessages(conv,msg, id);
    conv.append(msg);

    var messageForm = $(document.createElement('div'));
    messageForm.attr('class', 'messageForm');

    var form = $(document.createElement('form'));
    form.attr('action', '/MessMe/AddMessage/' + id);
    form.attr('data-ajax', 'true');
    form.attr('data-ajax-complete', 'done(this)');
    form.attr('data-ajax-mode', 'replace');
    form.attr('data-ajax-update', '#null');
    form.attr('method', 'post');

    var input = $(document.createElement('input'));
    input.attr('class', 'messageField');
    input.attr('name', 'message');
    input.attr('type', 'text');
    form.append(input);

    
    var imgInput = $(document.createElement('img'));
    imgInput.attr('class', 'uploadImgInput');
    imgInput.attr('src', '/Images/chatPicture.png');
    imgInput.on('click', function () {
        activatte(this);
    });

    var form2 = $(document.createElement('form'));

    messageForm.append(form);
    messageForm.append(form2);

    form2.append(imgInput);

    var fileInput = $(document.createElement('input'));
    fileInput.attr('type', 'file');
    fileInput.attr('name', 'datafile');
    fileInput.css('visibility', 'hidden');
    fileInput.on('change', function () {
        fileUpload(form2, '/Wtf/Upload/' + id, 'upload');
        return false;
    });
    form2.append(fileInput);
    conv.append(messageForm);
    $(targetDiv).append(conv);
}

function loadConversationMessages(conv, msg, id) {
    $.ajax({
        type: 'GET',
        url: '/MessMe/ConversationMessages/' + id,
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (i, message) {
                    appendMessage(message, msg);
                    $(conv).data('lastId', message.Id);
                });
            } else {
                $(conv).data('lastId', '0');
            }

            $('.messageImage').load(function () {
            });

            startLoadingMessages(conv, msg, id);
        },
        error: function () {
            alert('conv error');
        }
    });
}

function startLoadingMessages(conv, msg, id, lastId) {

    var url = '/MessMe/NewConversationMessages/' + id + '?lastId=' + lastId;
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            if (data.length > 0) {
                $.each(data, function (i, message) {
                    if (message.Id > lastId) {
                        appendMessage(message, msg);
                        lastId = message.Id;
                    }
                });
                $('.messageImage').load(function () {
                    $(msg).scrollTop(msg.prop('scrollHeight'));
                });
                $(msg).scrollTop(msg.prop('scrollHeight'));
            }
            
            startLoadingMessages(conv, msg, id, lastId);
        },
        error: function () {
        },
        timeout: 30000
    });
}

function hideConv(button, id) {
    var div = $(button).parent().parent();
    $(div).toggle();
    $.ajax({
        url: '/MessMe/RemoveConversation/' + id,
        success: function (data) {
        },
        error: function () {
        }
    });
}

function loadPartener(part, id) {
    $.ajax({
        type: 'GET',
        url: '/MessMe/ConversationPartener/' + id,
        success: function (data) {
            $(part).append(data.Name);
        },
        error: function () {
            $(part).append('Error');
        }
    });
}

function done() {
    $(".messageField").each(function () {
        $(this).val('');
    });
}

function appendMessage(message, msgDiv) {
    var textDiv;
    if (message.User) {
        var chatRight = $(document.createElement('div'));
        chatRight.attr('class', 'chatRight');

        var rightText = $(document.createElement('div'));
        rightText.attr('class', 'rightText');
        textDiv = rightText;
        $(chatRight).append(rightText);
        $(msgDiv).append(chatRight);
    } else {
        var chatRight = $(document.createElement('div'));
        chatRight.attr('class', 'chatLeft');

        var rightText = $(document.createElement('div'));
        rightText.attr('class', 'leftText');
        textDiv = rightText;
        $(chatRight).append(rightText);
        $(msgDiv).append(chatRight);
    }

    if (message.Message == null) {
        if (message.Image == null) {
            var id = message.Id;
            var donwloadUrl = '/Download/MessageImage/' + id;
            var url = '/Images/MessageImage/' + id;
            var link = $(document.createElement('a'));
            link.attr('href', donwloadUrl);
            var img = $(document.createElement('img'));
            $(img).attr('src', url);
            $(img).attr('class', 'messageImage');
            link.append(img);
            textDiv.append(link);
        } else {
            var img = $(document.createElement('img'));
            $(img).attr('src', 'data:image/jpg;base64,'+message.Image);
            $(img).attr('class', 'messageImage');
            textDiv.append(img);
        }
    } else {
        var text = message.Message;
        var words = text.split(' ');
        $.each(words, function (i, word) {
            var link = "http";
            if (word.indexOf(link) != -1) {
                var append = $(document.createElement('a'));
                append.attr('href', word);
                append.attr('target', '_blank');
                append.text(word);
                textDiv.append(append);

            } else {
                textDiv.append(word);
            }
            textDiv.append(' ');
        });
    }
    $('.messageImage').load(function () {
        $(msgDiv).scrollTop(msgDiv.prop('scrollHeight'));
    });
    $(msgDiv).scrollTop(msgDiv.prop('scrollHeight'));

    var interval = parseInt(message.Interval);
    if (interval > 0) {
        var timeout = interval * 1000;

        var remaining = interval - 1;

        var cron = $(document.createElement('div'));
        cron.css('color', 'red');
        cron.css('font-size', 8);
        cron.css('width', 20);
        cron.css('background-color', 'white');
        cron.css('text-align', 'center');
        cron.css('margin-bottom', 10);
        textDiv.prepend(cron);
        cron.text(remaining);
        $(msgDiv).scrollTop(msgDiv.prop('scrollHeight'));
        var cronFunc = setInterval(function () {
            cron.text(remaining);
            remaining = remaining - 1;
        }, 1000);

        if (interval > 0) {
            var time = setTimeout(function () {
                clearInterval(cronFunc);
                textDiv.hide();
                textDiv.remove();
            }, timeout);
        }
    }
    
}

function startPipeLongPolling() {
    /*if (active == false) {
        active = true;
        pipeLongPolling();
    }*/
}

var poolingDivs = [];
function addPolling(div) {
    poolingDivs.push(div);
    getDivs();
    startPipeLongPolling();
}

function removePolling(div) {
    poolingDivs.splice($.inArray(div, poolingDivs), 1);
}
function removePolling(div) {
}

var active = false;

function getDivs() {
    return poolingDivs;
}

function pipeLongPolling() {
    $.ajax({
        url: '/ChatData/PipeMessages/' + pipeLastId,
        async: true,
        success: function (data) {
            $.each(data, function (i, message) {
                var divId = '#conv-' + message.ConversationId;

                var div = mainDiv.find(divId);
                var msg = div.find('.msg');
                appendMessage(message, msg);
                });
                //});
                if (data.length > 0)
                    setPipeLastId(data.slice(-1)[0].Id);
                pipeLongPolling();

        },
        error: function () {
        }

    });

}

