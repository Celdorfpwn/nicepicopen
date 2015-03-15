function appendPictures(pictures, divElement) {
    $.each(pictures, function (i, pic) {

        var worldContainer = createDiv('worldContainer');
        $(divElement).append(worldContainer);

        var worldPictureDetails = createDiv('worldPictureDetails');
        $(worldContainer).append(worldPictureDetails);

        var worldPhotographerContent = createDiv('worldPhotographerContent');
        $(worldPictureDetails).append(worldPhotographerContent);

        var worldPhotographerDetails = createDiv('worldPhotographerDetails');
        $(worldPhotographerContent).append(worldPhotographerDetails);

        var worldPhotographerPictureContainer = createDiv('worldPhotographerPictureContainer');
        $(worldPhotographerDetails).append(worldPhotographerPictureContainer);

        var userurl = '/Images/UserProfileImage/' + pic.PhoId;
        var userimg = $(document.createElement('img'));
        userimg.attr('src', userurl);
        $(worldPhotographerPictureContainer).append(userimg);

        var userTextLink = createSpan('userTextLink');
        $(worldPhotographerDetails).append(userTextLink);


        var userLinkUrl = '/Explorer/Details/' + pic.PhoId;
        var userLinkName = 'by ' + pic.PhoName;
        var userLink = createLink(userLinkUrl, userLinkName);
        $(userTextLink).append(userLink);

        var albumLinkUrl = '/Explorer/Album/' + pic.AlbumId + '?picture=0';
        var albumLinkName = 'Album ' + pic.AlbumName;
        var albumLink = createLink(albumLinkUrl, albumLinkName);
        var albumSpan = createSpan('userAlbumLink');
        albumSpan.append(albumLink);
        $(worldPhotographerDetails).append('<br />');
        $(worldPhotographerDetails).append(albumSpan);


        var worldPictureStuff = createDiv('worldPictureStuff');
        $(worldPictureDetails).append(worldPictureStuff);
        var notUrl = '/WorkbenchPictures/NotSo/'+pic.PictureId;
        var notLink = createAjaxLink(notUrl, 'not', 'GET', 'replace', '#null');
        var notdiv = createDiv('notLink');

        var niceUrl = '/WorkbenchPictures/Nice/' + pic.PictureId;
        var niceLink = createAjaxLink(niceUrl, 'nice', 'GET', 'replace', '#null');
        var nicediv = createDiv('niceLink');

        $(notLink).append(notdiv);
        $(niceLink).append(nicediv);

        $(worldPictureStuff).append(notLink);
        $(worldPictureStuff).append(niceLink);

        var niceOrNot = createDiv('niceOrNot');
        var before = createDiv('before');
        var after = createDiv('after');
        if (pic.HasNice) {
            $(before).text('ice');
        } else if (pic.HasNot) {
            $(before).text('ot');
        } else {
        }
        $(niceOrNot).append(before);
        $(niceOrNot).append(after);
        $(worldPictureStuff).append(niceOrNot);

        var pictureContainer = createDiv('worldPictureContainer');
        $(worldContainer).append(pictureContainer);




        var url = '/Images/DetailsPictureWithWatermark/' + pic.PictureId;
        var img = $(document.createElement('img'));
        img.attr('src', url);
        $(pictureContainer).append(img);
    });
}

function createAjaxLink(url, className,method,mode,updateId) {
    var link = $(document.createElement('a'));
    link.attr('href', url);
    link.attr('class', className);
    link.attr('data-ajax', true);
    link.attr('data-ajax-method', method);
    link.attr('data-ajax-mode', mode);
    link.attr('data-ajax-update', updateId);

    return link;
}

function createLink(url, text) {
    var userLink = $(document.createElement('a'));
    userLink.attr('href', url);
    userLink.text(text);
    return userLink;
}

function createSpan(className) {
    var span = $(document.createElement('span'));
    span.attr('class', className);
    return span;
}

function createDiv(className) {
    var div = $(document.createElement('div'));
    div.attr('class', className);
    return div;
}

function requestPics(url, skip, div) {
        $.ajax({
            type: 'GET',
            url: url + skip,
            success: function (data) {
                appendPictures(data, div);
            },
            beforeSend: function () {
                $(window).unbind('scroll');
            },
            complete: function () {
                $(window).bind('scroll', scroll);
            },
            error: function () {
                alert("Load error");
            }
        });
}


var localurl;
var localskip;
var localdiv;

function initialize(url, div) {
    localurl = url;
    localskip = 0;
    localdiv = div;
    requestPics(localurl, localskip, localdiv);
    $(window).bind('scroll', scroll);
}

function scroll() {
    if ($(window).scrollTop() + $(window).height() > $(document).height()-50) {
        localskip = localskip + 10;
        requestPics(localurl, localskip, localdiv);
    }
}