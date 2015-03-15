using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesProvider.Concrete
{
    public static class ResizerContants
    {

        static AlbumProfileC albumProfile = new AlbumProfileC();
        static ImageCaruselC imageCarusel = new ImageCaruselC();
        static UserProfileC userProfile = new UserProfileC();
        static UserCameraC userCamera = new UserCameraC();
        static AlbumPictureCaruselC pictureCarusel = new AlbumPictureCaruselC();
        static PictureWithWatermarkDetailsC pictureWithWatermarkDetails = new PictureWithWatermarkDetailsC();
        static UserProfileSmall userProfileSmall = new UserProfileSmall();

        class PictureWithWatermarkDetailsC : Constant
        {
            const string width = "1000";
            const string height = "1000";

            public string Width
            {
                get { return width; }
            }

            public string Height
            {
                get { return height; }
            }
        }

        class AlbumPictureCaruselC : Constant
        {
            const string width = "500";
            const string height = "500";

            public string Width
            {
                get { return width; }
            }

            public string Height
            {
                get { return height; }
            }
        }

        class ImageCaruselC : Constant
        {
            public const string width = "200";
            public const string height = "200";

            public string Width
            {
                get { return width; }
            }

            public string Height
            {
                get { return height; }
            }
        }

        class UserProfileSmall : Constant
        {
            public const string width = "50";
            public const string height = "50";

            public string Width
            {
                get { return width; }
            }

            public string Height
            {
                get { return height; }
            }
        }

            class AlbumProfileC : Constant
            {
                const string width = "500";
                const string height = "500";

                public string Width
                {
                    get { return width; }
                }

                public string Height
                {
                    get { return height; }
                }
            }

            class UserProfileC : Constant
            {
                public const string width = "100";
                public const string height = "100";

                public string Width
                {
                    get { return width; }
                }

                public string Height
                {
                    get { return height; }
                }
            }

            class UserCameraC : Constant
            {
                const string width = "200";
                const string height = "180";

                public string Width
                {
                    get { return width; }
                }

                public string Height
                {
                    get { return height; }
                }
            }

            public static Constant PictureWithWatermarkDetails
            {
                get
                {
                    return pictureWithWatermarkDetails;
                }
            }

            public static Constant AlbumPictureCarusel
            {
                get
                {
                    return pictureCarusel;
                }
            }

            public static Constant ImageCarusel
            {
                get
                {
                    return imageCarusel;
                }
            }

            public static Constant AlbumProfile
            {
                get
                {
                    return albumProfile;
                }
            }

            public static Constant UserProfile
            {
                get
                {
                    return userProfile;
                }
            }

            public static Constant UserCamera
            {
                get
                {
                    return userCamera;
                }
            }

            public static Constant UserProfileVerySmall
            {
                get
                {
                    return userProfileSmall;
                }
            }
        }
    }

    public interface Constant
    {
        string Width { get; }
        string Height { get; }
    }