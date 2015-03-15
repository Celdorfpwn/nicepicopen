

window.ContentExtractor = new Extractor();
window.Templates = new Templatee();
window.processing = true;
window.stopExtract = false;
window.Finder = new Finder();

$('.workbenchProfileLink').click(function () {
    window.ContentExtractor.disable();
});



function Builderr(type) {
    this.createAjaxLink = function (url, className, method, mode, updateId) {
        var link = $(document.createElement('a'));
        link.attr('href', url);
        link.attr('class', className);
        link.attr('data-ajax', true);
        link.attr('data-ajax-method', method);
        link.attr('data-ajax-mode', mode);
        link.attr('data-ajax-update', updateId);
        return link;
    }

    this.createLink = function (url, text) {
        var userLink = $(document.createElement('a'));
        userLink.attr('href', url);
        userLink.text(text);
        return userLink;
    }

    this.createSpan = function (className, id) {
        var span = $(document.createElement('span'));
        span.attr('class', className);
        return span;
    }

    this.createDiv = function (className, id) {
        var div = $(document.createElement('div'));
        div.attr('class', className);
        div.attr('id', id);
        return div;
    }

    this.createImg = function (src, className) {
        var img = $(document.createElement('img'));
        img.attr('src', src);
        img.attr('class', className);
        return img;
    }
}

function Finder() {
    this.findInDiv = function (where, className, data, search) {
        var searchClass = '.' + className;
        var divs = $(where).find(searchClass);
        var found = null;
        $.each(divs, function (i, div) {
            if ($(div).data(data) == search) {
                found = div;
            }
        });
        return found;
    }
}

function Templatee(type) {
    this.appendPictures = function (pictures, divElement) {
        var provider = "";
        var maxheight = $(window).height() / 1.05;
        var builder = new Builderr();
        $.each(pictures, function (i, pic) {

            var worldContainer = builder.createDiv('worldContainer');
            worldContainer.data('picture', pic.PictureId);

            $(divElement).append(worldContainer);

            var worldPictureDetails = builder.createDiv('worldPictureDetails');
            $(worldContainer).append(worldPictureDetails);

            var worldPhotographerContent = builder.createDiv('worldPhotographerContent');
            $(worldPictureDetails).append(worldPhotographerContent);

            var worldPhotographerDetails = builder.createDiv('worldPhotographerDetails');
            $(worldPhotographerContent).append(worldPhotographerDetails);

            var worldPhotographerPictureContainer = builder.createDiv('worldPhotographerPictureContainer');
            $(worldPhotographerDetails).append(worldPhotographerPictureContainer);

            var userurl = '/Images/UserProfileImage/' + pic.PhoId;

            worldPhotographerPictureContainer.css('background', 'url(' + userurl + ') center');

            var userTextLink = builder.createDiv('userTextLink');
            $(worldPhotographerDetails).append(userTextLink);


            var userLinkUrl = '/Profile/UserView/' + pic.PhoId;
            var userLinkName = 'by ' + pic.PhoName;
            //var userLink = builder.createLink(userLinkUrl, userLinkName);
            var userLink = builder.createAjaxLink(userLinkUrl, '', 'GET', 'replace', '#workbench');
            userLink.text(userLinkName);
            $(userTextLink).append(userLink);

            var albumLinkUrl = '/Explorer/Album/' + pic.AlbumId + '?picture=0';
            var albumLinkName = 'Album "' + pic.AlbumName+'"';
            var albumLink = builder.createLink(albumLinkUrl, albumLinkName);
            var albumSpan = builder.createDiv('userAlbumLink');
            albumSpan.append(albumLink);

            var categoryLink = builder.createLink(albumLinkUrl, 'Category "' + pic.CategoryName + '"');
            var categorySpan = builder.createDiv('userAlbumLink');
            categorySpan.append(categoryLink);

            $(worldPhotographerDetails).append(albumSpan);
            $(worldPhotographerDetails).append(categorySpan);

            var worldPictureStuff = builder.createDiv('worldPictureStuff');

            var notdiv = builder.createDiv('notLink');

            var nicediv = builder.createDiv('niceLink');

            $(worldPictureStuff).append(notdiv);
            $(worldPictureStuff).append(nicediv);

            var niceOrNot = builder.createDiv('niceOrNot');
            var before = builder.createDiv('before');
            var after = builder.createDiv('after');
            if (pic.HasNice) {
                $(before).text('ice');
            } else if (pic.HasNot) {
                $(before).text('ot');
            } else {
            }
            $(niceOrNot).append(before);
            $(niceOrNot).append(after);
            $(worldPictureStuff).append(niceOrNot);

            var pictureContainer = builder.createDiv('worldPictureContainer');
            $(worldContainer).append(pictureContainer);

            var loadingUrl = '/Images/loading.gif'
            var loadingImg = builder.createImg(loadingUrl, 'loadingImgCont');
            $(pictureContainer).append(loadingImg);
            
            var url = PICTURES_URL+"/Pictures/Picture/" + pic.PictureId;

            var img = builder.createImg(url);
 
            img.css('display', 'none');
            $(pictureContainer).append(img);

            $(img).load(function () {
                $(loadingImg).hide();
                $(img).show();
            });


            var worldPictureDetails2 = builder.createDiv('worldPictureDetails');
            $(worldContainer).append(worldPictureDetails2);
            var moreStuff = builder.createDiv('morePictureStuff');

            $(worldContainer).append(moreStuff);


            $.each(pic.Nicers, function (i, nice) {
                var nicer = builder.createDiv('morePictureStuffImg');
                var niceUrl = PICTURES_URL + '/Pictures/SmallProfile/' + nice;
                nicer.data('id', 'nice-' + nice);
                nicer.css('background', 'url(' + niceUrl + ') center');
                moreStuff.append(nicer);

            });
            var count = builder.createDiv('morePictureStuffCount');
            count.text(pic.Nicers.length);

            moreStuff.on('click', function () {
                if ($(this).height() == 48) {
                    $(this).attr('class', 'morePictureStuffToggle');
                } else {
                    $(this).attr('class', 'morePictureStuff');
                }
            });

            $(worldPictureDetails2).append(worldPictureStuff);
            $(worldPictureDetails2).append(count);

            nicediv.on('click', function () {
                $.ajax({
                    type: 'GET',
                    url: '/WorkbenchPictures/Nice/' + pic.PictureId,
                    success: function (data) {
                        if (data != null) {
                            window.Templates.addNice(data.Id, worldContainer);
                            theChat.niceToAll(pic.PictureId, data.Id);
                        }

                    },
                    error: function () {
                    }
                });
            });


            notdiv.on('click', function () {
                $.ajax({
                    type: 'GET',
                    url: '/WorkbenchPictures/NotSo/' + pic.PictureId,
                    success: function (data) {

                        window.Templates.addNot(data.Id, worldContainer);
                        theChat.notToAll(pic.PictureId, data.Id);

                       /* var search = 'nice-' + data.Id;
                        var div = window.Finder.findInDiv(moreStuff, 'morePictureStuffImg', 'id', search);
                        if (div != null) {
                            $(div).hide('fast');
                            $(div).remove();
                            var countText = count.text();
                            var nextText = parseInt(countText);
                            count.text(nextText - 1);
                        }*/
                    }
                });
            });
        });
    }

    this.addNice = function (pictureId, worldContainer) {
        var builder = new Builderr();
        var nicer = builder.createDiv('morePictureStuffImg');
        nicer.data('id', 'nice-' + pictureId);
        var niceUrl = PICTURES_URL + '/Pictures/SmallProfile/' + pictureId;
        nicer.css('background', 'url(' + niceUrl + ') center');
        var moreStuff = $(worldContainer).find('.morePictureStuff');

        moreStuff.prepend(nicer);
        var count = $(worldContainer).find('.morePictureStuffCount');
        var countText = count.text();
        var nextText = parseInt(countText);
        count.text(nextText + 1);
    }

    this.addNot = function (userId, worldContainer) {
        var search = 'nice-' + userId;
        var div = window.Finder.findInDiv(worldContainer, 'morePictureStuffImg', 'id', search);
        if (div != null) {
            $(div).hide('fast');
            $(div).remove();

            var count = $(worldContainer).find('.morePictureStuffCount');
            var countText = count.text();
            var nextText = parseInt(countText);
            count.text(nextText - 1);
        }
    }

    this.appendAllPictures = function (pictures, div) {

        var builder = new Builderr();
        var height = $(div).width() / 3;
        $.each(pictures, function (i, pic) {
            var pictureLinkUrl = '/WorkbenchPictures/PictureProfile/' + pic.Id;
            var pictureLink = builder.createLink(pictureLinkUrl, '');
            $(div).append(pictureLink);

            var albumPictureContainer = builder.createDiv('albumPictureContainer');

            var url = 'url(/Images/AlbumPictureCarusel/' + pic.Id + ') center';
            albumPictureContainer.css('background', url);
            albumPictureContainer.css('background-color', 'black');
            albumPictureContainer.css('background-size', 'cover');
            albumPictureContainer.css('height', height);

            $(pictureLink).append(albumPictureContainer);
        });
    }

    this.appendAlbums = function (albums, div) {
        
        var builder = new Builderr();
        $.each(albums, function (i, album) {
            var workbenchAlbumShow = builder.createDiv('workbenchAlbumShow');
            var albumProfilePicture = builder.createDiv('albumProfilePicture');
            $.ajax({
                type: 'GET',
                url: '/WorkbenchAlbums/AlbumProfilePicture/' + album.Id,
                success: function (data) {
                    var picUrl = '/Images/AlbumPictureCarusel/' + data.Id;
                    var albumProfileImg = builder.createImg(picUrl, 'albumProfile');
                    var url = 'url("/Images/AlbumPictureCarusel/' + data.Id + '")';
                    $(albumProfilePicture).css('background', url);
                    $(albumProfilePicture).css('background-size', 'cover');
                },
                error: function () {
                }
            });
            $(workbenchAlbumShow).append(albumProfilePicture);
            var albumDescription = builder.createDiv('albumDescription');
            $(albumDescription).append('<p>Album : ' + album.Name + '</p>');
            $(workbenchAlbumShow).append(albumDescription);
            
            var albumQuickBar = builder.createDiv('albumQuickBar');
            var addLinkP = $(document.createElement('p'));
            var addLink = $(document.createElement('a'));
            $(addLink).attr('href', '/WorkbenchPictures/AddPictures/' + album.Id);
            $(addLink).text('Add pictures');
            $(addLinkP).append(addLink);
            $(albumQuickBar).append(addLinkP);
       
            var downloadLinkP = $(document.createElement('p'));
            var downloadLink = $(document.createElement('a'));
            $(downloadLink).attr('href', '/WorkbenchAlbums/Download/' + album.Id);
            $(downloadLink).text('Download');
            $(downloadLinkP).append(downloadLink);
            $(albumQuickBar).append(downloadLinkP);

            $(workbenchAlbumShow).append(albumQuickBar);
            var albumCaruselBar = builder.createDiv('albumCaruselBar');

            $.ajax({
                type: 'GET',
                url: '/WorkbenchAlbums/AlbumCaruselPictures/' + album.Id,
                success: function (data) {
                    $.each(data, function (i, picture) {
                        var div = builder.createDiv('caruselContainer');
                        var url = 'url("/Images/AlbumPictureCarusel/' + picture.Id + '")';
                        $(div).css('background', url);
                        $(div).css('background-size', 'cover');
                        $(albumCaruselBar).append(div);
                    });
                },
                error: function () {
                }
            });

            $(workbenchAlbumShow).append(albumCaruselBar);
            $(div).append(workbenchAlbumShow);
        });
    }
}

function Extractor(type) {
    var pageInfo = {
        loading: '',
        localUrl: '',
        localSkip: '',
        localDiv: '',
        localLoading: '',
        template: '',
        skipMult :'',
        scroll:'',
        Init: function (url, div, loading, template,skip,scroll) {
            pageInfo.loading = true;
            pageInfo.localUrl = url;
            pageInfo.localSkip = 0;
            pageInfo.localDiv = div;
            pageInfo.localLoading = loading;
            pageInfo.template = template;
            pageInfo.skipMult = skip;
            $(pageInfo.localLoading).hide();
        },
        Disable: function () {
            pageInfo.loading = '';
            pageInfo.localUrl = '';
            pageInfo.localSkip = '';
            pageInfo.localDiv = '';
            pageInfo.localLoading = '';
            pageInfo.template = '';
            pageInfo.skipMult = '';
        }

    }
    this.disable = function(){
        pageInfo.Disable();
    }

    this.init = function (url, div, template, skipCount, loadingDiv, scroll) {
        pageInfo.Init(url, div, loadingDiv, template, skipCount, scroll);
        window.processing = true;
        window.stopExtract = false;
        if (scroll) {
            $(window).bind('scroll', function () {
                window.ContentExtractor.scroll();
            });
        }

        this.request();
    }

 
    this.scroll = function() {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 50) {
            if (window.processing) {
                pageInfo.localSkip = pageInfo.localSkip + pageInfo.skipMult;
                this.request();
            }
        }
    }

    this.request = function () {
        if (pageInfo.loading) {
            var url = pageInfo.localUrl + pageInfo.localSkip;

            $.ajax({
                type: 'GET',
                url: url,
                dataType: 'JSON',
                contentType:'application/json',
                success: function (data) {
                    if (data.length == 0) {
                        window.stopExtract = true;
                    } else {
                        pageInfo.template(data, pageInfo.localDiv);
                    }
                },
                beforeSend: function () {
                    window.processing = false;
                    $(pageInfo.localLoading).show();
                },
                complete: function () {
                    if (pageInfo.loading) {
                        
                    } else {
                        var end = $(document.createElement('div'));
                        $(end).attr('class', 'endDiv1');
                        $(pageInfo.localDiv).append(end);
                        $(pageInfo.localLoading).hide();
                    }
                    $(pageInfo.localLoading).hide();
                    if (window.stopExtract) {
                        window.processing = false;
                    } else {
                        window.processing = true;
                    }
                },
                error: function (xhr,textstatus,errorThrown) {
                    alert(xhr.statusText+' '+textstatus+ ' ' + errorThrown);
                }
            });
        }
    }




}








