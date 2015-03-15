$('#selectFile').click(function () {
    $('#fileInput').trigger('click');
});

// load
$(function () {
    var div = $('#prtfImage');
    var id = div.data('id');
    var rand = Math.random();
    var profileImage = $(document.createElement('img'));
    $(profileImage).attr('src', PICTURES_URL+'/Pictures/Profile/'+id+'?rand='+rand);
    $(profileImage).attr('id', 'workbenchProfileImage');
    div.append(profileImage);
    $('#workbenchNews').css('height', 230);
});

// load
$(function () {
    var theUrl = '/World/Pictures/';
    var url2 = '/MessMe/Conversations/';
    $.ajax({
        url: url2,
        success: function (data) {
            $("#conversations").append(data);
        }
    });

    $.ajax({
        url: '/ExplorerUpdates/News/',
        success: function (data) {
            $("#workbenchNews").append(data);
        }
    });

    var targetDiv = $('#conversationsDiv');
    $.ajax({
        url: '/ChatData/AllConversations/',
        success: function (data) {
            $.each(data, function (i, conv) {
                writeConversationFirst(targetDiv, conv);
            });
        },
        error: function () {
            alert('wtf');
        }
    });

    $.ajax({
        url: theUrl,
        success: function (data) {
            $("#workbenchContent").append(data);
        }
    });
});

function writeConversationFirst(targetDiv, conversation) {

    var conversationDiv = $(document.createElement('div'));
    conversationDiv.attr('class', 'conversationMessages');
    conversationDiv.attr('id', 'conv-' + conversation.Id);
    conversationDiv.data('id', conversation.Id);

    conversationPartener = $(document.createElement('div'));
    conversationPartener.attr('class', 'conversationPartener');
    conversationPartener.text(conversation.Name);
    var hideButton = $(document.createElement('button'));
    hideButton.attr('class', 'conversationHide');
    hideButton.on('click', function () {
        hideConv($(this), conversation.Id);
    });
    hideButton.text('X');

    var recordButton = $(document.createElement('button'));
    recordButton.attr('class', 'conversationHide');
    recordButton.on('click', function () {
        recording($(this));
    });
    recordButton.text('R');

    var selectOption = $(document.createElement('select'));
    selectOption.attr('class', 'selectLifeInterval');

    selectOption.on('change', function () {
        changeInterval($(this));
    });

    var noneOption = $(document.createElement('option'));
    noneOption.text('none');
    noneOption.val('0');
    selectOption.append(noneOption);
    for (var i = 20 ; i < 200; i += 20) {
        var anOption = $(document.createElement('option'));
        anOption.text(i + ' sec');
        anOption.val(i);
        selectOption.append(anOption);
    }
    





    conversationPartener.append(hideButton);
    conversationPartener.append(recordButton);
    conversationPartener.append(selectOption);

    conversationDiv.append(conversationPartener);


    msgDiv = $(document.createElement('div'));
    msgDiv.attr('class', 'msg');
    msgDiv.data('id', conversation.Id);

    conversationDiv.append(msgDiv);

    $.each(conversation.Messages, function (i, message) {
        appendMessage(message, msgDiv);
        $(msgDiv).scrollTop(msgDiv.prop('scrollHeight'));
    });

    var formsDiv = $(document.createElement('div'));
    formsDiv.attr('class', 'messageForm');

    var textForm = $(document.createElement('form'));
    textForm.attr('class', 'textMessageForm');
    textForm.data('user', conversation.PartenerId);
    textForm.data('conversation', conversation.Id);
    textForm.data('myid', conversation.MyId);
    textForm.data('record', 'true');
    textForm.data('inteval', '0');
    textForm.on('submit', function () {
        submitMessage($(this));
        return false;
    });

    var textFormInput = $(document.createElement('input'));
    textFormInput.attr('type', 'text');
    textFormInput.attr('class', 'messageField');
    textFormInput.attr('name', 'message');

    textForm.append(textFormInput);

    var fileForm = $(document.createElement('form'));

    var inputImg = $(document.createElement('img'));
    inputImg.css('cursor', 'pointer');
    inputImg.css('width', 30);
    inputImg.css('height', 30);
    inputImg.attr('src', '/Images/chatPicture.png');
    inputImg.on('click', function () {
        activatte($(this));
    });


    fileForm.append(inputImg);

    var fileFormInput = $(document.createElement('input'));
    fileFormInput.attr('type', 'file');
    fileFormInput.css('visibility', 'hidden');
    fileFormInput.attr('name', 'datafile');
    fileFormInput.on('change', function () {
        var url = '/Wtf/Upload/' + conversation.Id + '?userId=' + conversation.PartenerId;
        fileUpload($(this).parent(),url, 'upload');
    });

    fileForm.append(fileFormInput);

    formsDiv.append(textForm);
    formsDiv.append(fileForm);


    conversationDiv.append(formsDiv);

    targetDiv.append(conversationDiv);

    if (conversation.Active) {
    } else {
        $(conversationDiv).hide();
    }


}


function scrollWindowTop() {
    $(window).scrollTop(0);
}
$('#BestLink').click(function () {
    scrollWindowTop();
});
$('#NewestLink').click(function () {
    scrollWindowTop();
});


$('#FreeViewLink').click(function () {
    scrollWindowTop();
});
$('#FavoritesViewLink').click(function () {
    scrollWindowTop();

});

$('#discussionsLink').click(function () {
    scrollWindowTop();
});


$(".workbenchProfileLink").mouseover(function () {
    $(this).find(".workbenchProfileTheLink").css("color", "white");
    $(this).find(".workbenchProfileNewPicture").attr("class", "workbenchProfileNewPictureReplace");
});
$(".workbenchProfileLink").mouseout(function () {
    $(this).find(".workbenchProfileTheLink").css("color", "black");
    $(this).find(".workbenchProfileNewPictureReplace").attr("class", "workbenchProfileNewPicture");
});
$(".pictureNotSoNiceLink").click(function () {
    $(this).parent().parent().find('a').find(".pictureNiceLink").css("background-color", "grey");
    $(this).parent().parent().find('a').find(".pictureNiceLink").css("color", "black");
    $(this).css("background-color", "#9400D3");
    $(this).css("color", "white");
});
$(".pictureNiceLink").click(function () {
    $(this).parent().parent().find('a').find(".pictureNotSoNiceLink").css("background-color", "grey");
    $(this).parent().parent().find('a').find(".pictureNotSoNiceLink").css("color", "black");
    $(this).css("background-color", "#9400D3");
    $(this).css("color", "white");
});