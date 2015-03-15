


var picturesUrl;
var picturesDiv;
var picturesSkip;
var picturesLoading;
var loading;
function initializePicturesAll(url, div, loadingDiv) {
    picturesUrl = url;
    picturesDiv = div;
    picturesSkip = 0;
    loading = true;
    picturesLoading = loadingDiv;
    picturesRequest(picturesUrl, picturesSkip, picturesDiv);
    $(window).bind('scroll', scrollWin);
}

function picturesRequest(url, skip, div) {
    if (loading) {
        $.ajax({
            type: 'GET',
            url: url + skip,
            success: function (data) {
                if (data.length == 0)
                    loading = false;
                appendAllPictures(data, div);
            },
            beforeSend: function () {
                $(picturesLoading).show();
                $(window).unbind('scroll');
            },
            complete: function () {
                $(picturesLoading).hide();
                if (loading) {
                    $(window).bind('scroll', scrollWin);
                } else {
                    var endDiv = $(document.createElement('div'));
                    $(endDiv).attr('class', 'endDiv');
                    $(div).append(endDiv);
                }
            },
            error: function () {
            }
        });
    }
}

function appendAllPictures(pictures, div) {
    var wheight = $(window).height();
    var widht = $(window).width();
    var height = $(window).height() / (widht / wheight) / 1.5;
    $.each(pictures, function (i, pic) {
        var pictureLinkUrl = '/WorkbenchPictures/PictureProfile/' + pic.Id;
        var pictureLink = $(document.createElement('a'));
        pictureLink.attr('href', pictureLinkUrl);
        $(div).append(pictureLink);

        var albumPictureContainer = $(document.createElement('div'));
        albumPictureContainer.css('background-color', 'black');
        albumPictureContainer.attr('class', 'albumPictureContainer');
        albumPictureContainer.css('height', height);
        var url = 'url(/Images/AlbumPictureCarusel/' + pic.Id + ') center';
        albumPictureContainer.css('background', url);
        albumPictureContainer.css('background-repeat', 'no-repeat');


        $(pictureLink).append(albumPictureContainer);
        
    });
}


function scrollWin() {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 50) {
        picturesSkip = picturesSkip + 27;
        picturesRequest(picturesUrl, picturesSkip, picturesDiv);
    }
}