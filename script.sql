USE [master]
GO
/****** Object:  Database [DoctorSkin]    Script Date: 14/03/2023 09:24:55 ******/
CREATE DATABASE [DoctorSkin]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DoctorSkin', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MYSQL\MSSQL\DATA\DoctorSkin.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DoctorSkin_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MYSQL\MSSQL\DATA\DoctorSkin_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DoctorSkin] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DoctorSkin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DoctorSkin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DoctorSkin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DoctorSkin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DoctorSkin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DoctorSkin] SET ARITHABORT OFF 
GO
ALTER DATABASE [DoctorSkin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DoctorSkin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DoctorSkin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DoctorSkin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DoctorSkin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DoctorSkin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DoctorSkin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DoctorSkin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DoctorSkin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DoctorSkin] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DoctorSkin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DoctorSkin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DoctorSkin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DoctorSkin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DoctorSkin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DoctorSkin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DoctorSkin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DoctorSkin] SET RECOVERY FULL 
GO
ALTER DATABASE [DoctorSkin] SET  MULTI_USER 
GO
ALTER DATABASE [DoctorSkin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DoctorSkin] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DoctorSkin] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DoctorSkin] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DoctorSkin] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DoctorSkin] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DoctorSkin', N'ON'
GO
ALTER DATABASE [DoctorSkin] SET QUERY_STORE = OFF
GO
USE [DoctorSkin]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[sttbill] [int] IDENTITY(1,1) NOT NULL,
	[idp] [int] NULL,
	[quantity] [int] NULL,
	[totalmoney] [nvarchar](max) NULL,
	[idbill] [nvarchar](max) NULL,
	[iduser] [nvarchar](max) NULL,
	[yesfb] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogContent]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogContent](
	[idbc] [int] IDENTITY(1,1) NOT NULL,
	[idb] [int] NULL,
	[contentblog] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogDetails]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogDetails](
	[idbt] [int] NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[shortcontent] [nvarchar](max) NOT NULL,
	[cardimg] [nvarchar](max) NOT NULL,
	[hideblog] [bit] NULL,
	[idb] [int] IDENTITY(1,1) NOT NULL,
	[date_up] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogTypes]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogTypes](
	[idbt] [int] NOT NULL,
	[namebt] [nvarchar](max) NOT NULL,
	[hide] [bit] NOT NULL,
	[meta] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bought]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bought](
	[iduser] [nvarchar](max) NULL,
	[datebuy] [datetime] NULL,
	[status] [nvarchar](max) NULL,
	[datestatus] [datetime] NULL,
	[sttbill] [int] NULL,
	[sttbought] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[typep] [int] NOT NULL,
	[namec] [nvarchar](50) NOT NULL,
	[hide] [bit] NOT NULL,
	[date_create] [nvarchar](50) NULL,
	[meta] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[iddoc] [int] IDENTITY(1,1) NOT NULL,
	[namedoc] [nvarchar](max) NULL,
	[infordoc] [nvarchar](max) NULL,
	[ava_doc] [nvarchar](max) NULL,
	[hide_doc] [bit] NULL,
	[date_up_doc] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[sttfb] [int] IDENTITY(1,1) NOT NULL,
	[idbill] [int] NULL,
	[cmt] [nvarchar](max) NULL,
	[datefb] [datetime] NULL,
	[hidefb] [bit] NULL,
	[iduser] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medias]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medias](
	[idmedia] [int] IDENTITY(1,1) NOT NULL,
	[hrefmedia] [nvarchar](max) NULL,
	[imgmedia] [nvarchar](max) NULL,
	[hidemedia] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[idp] [int] IDENTITY(1,1) NOT NULL,
	[namep] [nvarchar](100) NOT NULL,
	[typep] [int] NOT NULL,
	[newprice] [nvarchar](max) NOT NULL,
	[oldprice] [nvarchar](max) NOT NULL,
	[descr] [nvarchar](max) NOT NULL,
	[date_create] [nvarchar](max) NULL,
	[hide] [bit] NOT NULL,
	[statep] [nvarchar](max) NOT NULL,
	[img] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[name_dt] [nvarchar](max) NULL,
	[desc_dt] [nvarchar](max) NULL,
	[hide_dt] [bit] NULL,
	[img_dt] [nvarchar](max) NULL,
	[id_dt] [int] IDENTITY(1,1) NOT NULL,
	[meta] [nvarchar](max) NULL,
	[slider_dt] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicesDetails]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicesDetails](
	[id_sd] [int] IDENTITY(1,1) NOT NULL,
	[name_sd] [nvarchar](max) NULL,
	[img_sd] [nvarchar](max) NULL,
	[hide_sd] [bit] NULL,
	[price_sd] [nvarchar](max) NULL,
	[id_dt] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/03/2023 09:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[iduser] [nvarchar](max) NULL,
	[name] [nvarchar](max) NULL,
	[birth] [datetime] NULL,
	[gender] [nvarchar](10) NULL,
	[phone] [nvarchar](10) NULL,
	[email] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[block] [bit] NULL,
	[ava] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([sttbill], [idp], [quantity], [totalmoney], [idbill], [iduser], [yesfb]) VALUES (1, 1, 1, N'300000', N'1547451', N'htye7go15', N'Đã đánh giá')
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogDetails] ON 

INSERT [dbo].[BlogDetails] ([idbt], [title], [shortcontent], [cardimg], [hideblog], [idb], [date_up]) VALUES (1, N'Combo Eucerin trị mụn hiệu quả bạn nên thử', N'Eucerin là thương hiệu dược mỹ phẩm nổi tiếng ở Đức, cho ra đời nhiều sản phẩm với thành phần lành tính, an toàn cho làn da. Bộ sản phẩm chăm sóc da mụn từ sữa rửa mặt Eucerin, kem trị mụn Eucerin, kem chống nắng Eucerin luôn được Bác sĩ Da liễu khuyên dùng.', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2023/02/huong-dan-cham-soc-da-voi-san-pham-eucerin.jpg', 0, 1, CAST(N'2023-03-11T00:00:00.000' AS DateTime))
INSERT [dbo].[BlogDetails] ([idbt], [title], [shortcontent], [cardimg], [hideblog], [idb], [date_up]) VALUES (1, N'The Ordinary Hyaluronic Acid 2 + B5 dùng cho da mụn được không?', N'The Ordinary Hyaluronic Acid 2 + B5 có thật sự hiệu quả trong việc cấp ẩm và phục hồi làn da không? Vậy thực tế serum hyaluronic acid có dùng cho da mụn được không?', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2023/02/the-ordinary-hyaluronic-acid-2-b5-serum-co-tot-khong-370x210-1.jpg', 0, 2, CAST(N'2023-03-10T00:00:00.000' AS DateTime))
INSERT [dbo].[BlogDetails] ([idbt], [title], [shortcontent], [cardimg], [hideblog], [idb], [date_up]) VALUES (1, N'Top 8 kem trị mụn tốt nhất được Bác sĩ khuyên dùng', N'Kem trị mụn là sản phẩm không thể thiếu với bất kì ai đang bị mụn. Đây cũng là giải pháp đơn giản nhất, dễ thực hiện để loại bỏ mụn trứng cá hiệu quả. Tuy nhiên, khi sử dụng cần lựa chọn sản phẩm phù hợp với từng loại mụn và từng loại da khác nhau. Đặc biệt khi mua kem trị mụn ở hiệu thuốc mà không có sự tư vấn, chỉ định của bác sĩ thì càng cần phải tìm hiểu kĩ để tránh việc da bị kích ứng hoặc nổi mụn nhiều hơn. Cùng tham khảo top 8 kem trị mụn tốt nhất thường xuyên được sử dụng và cho hiệu quả cao. ', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2023/02/top-8-kem-tri-mun-tot-nhat-duoc-bac-si-khuyen-dung-370x210-2.jpg', 0, 3, CAST(N'2023-02-17T00:00:00.000' AS DateTime))
INSERT [dbo].[BlogDetails] ([idbt], [title], [shortcontent], [cardimg], [hideblog], [idb], [date_up]) VALUES (6, N'Top 5 miếng dán mụn hiệu quả nhất', N'Miếng dán mụn cóc là sản phẩm che khuyết điểm, hỗ trợ trị mụn. Top miếng dán mụn cosrx, acnes, some by mi hiệu quả', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2022/08/top-5-mieng-dan-mun-hieu-qua-nhat-370x210-1.jpg', 0, 8, CAST(N'2023-03-10T00:00:00.000' AS DateTime))
INSERT [dbo].[BlogDetails] ([idbt], [title], [shortcontent], [cardimg], [hideblog], [idb], [date_up]) VALUES (2, N'Review Top 05 serum Vitamin C cho da dầu mụn', N'Review serum vitamin c tốt nhất cho da dầu mụn. Serum balance vitamin c có tác dụng gì? Hướng dẫn sử dụng vitamin c hiệu quả tại nhà', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2022/09/review-05-serum-vitamin-c-cho-da-dau-mun-370x210-1.jpg', 0, 9, CAST(N'2023-03-11T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[BlogDetails] OFF
GO
INSERT [dbo].[BlogTypes] ([idbt], [namebt], [hide], [meta]) VALUES (1, N'Blog trị mụn', 0, N'blog-tri-mun')
INSERT [dbo].[BlogTypes] ([idbt], [namebt], [hide], [meta]) VALUES (2, N'Chăm sóc da', 0, N'cham-soc-da')
INSERT [dbo].[BlogTypes] ([idbt], [namebt], [hide], [meta]) VALUES (3, N'Blog trị sẹo', 0, N'blog-tri-seo')
INSERT [dbo].[BlogTypes] ([idbt], [namebt], [hide], [meta]) VALUES (4, N'Các phương pháp trị mụn', 0, N'phuong-phap-tri-mun')
INSERT [dbo].[BlogTypes] ([idbt], [namebt], [hide], [meta]) VALUES (5, N'Kiến thức thuốc trị mun', 0, N'kien-thuc-tri-mun')
INSERT [dbo].[BlogTypes] ([idbt], [namebt], [hide], [meta]) VALUES (6, N'Khác', 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Bought] ON 

INSERT [dbo].[Bought] ([iduser], [datebuy], [status], [datestatus], [sttbill], [sttbought]) VALUES (N'htye7go15', CAST(N'2023-03-10T00:00:00.000' AS DateTime), N'Hoàn thành', CAST(N'2023-03-11T00:00:00.000' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Bought] OFF
GO
INSERT [dbo].[Categories] ([typep], [namec], [hide], [date_create], [meta]) VALUES (1, N'Kem dưỡng ẩm', 0, NULL, N'kem-duong-am')
INSERT [dbo].[Categories] ([typep], [namec], [hide], [date_create], [meta]) VALUES (2, N'Kem chống nắng', 0, NULL, N'kem-chong-nang')
INSERT [dbo].[Categories] ([typep], [namec], [hide], [date_create], [meta]) VALUES (3, N'Serum', 0, NULL, N'serum')
INSERT [dbo].[Categories] ([typep], [namec], [hide], [date_create], [meta]) VALUES (4, N'Sửa rửa mặt', 0, NULL, N'sua-rua-mat')
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (1, N'Nguyễn Thị Anh Thư', N'Kinh nghiệm 30 năm trong điều trị da liễu, 7 năm trực tiếp khám và điều trị mụn cho hàng ngàn Khách hàng tại O2 SKIN', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-SANG.png', 0, CAST(N'2023-02-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (2, N'Võ Thanh Toàn', N'Kinh nghiệm gần 10 năm trong lĩnh vực da liễu, là một trong những Bác sĩ đầu tiên công tác tại Phòng Khám O2 SKIN, chi nhánh Cần Thơ', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-TOAN.png', 0, CAST(N'2023-02-19T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (3, N'Nguyễn Thị Thanh Vân', N'Kinh nghiệm 30 năm trong điều trị da liễu, chuyên môn cao và tay nghề giỏi, Bác sĩ Nguyễn Thị Thanh Vân là một trong những Bác sĩ kỳ cựu tại O2 SKIN.', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-VAN.png', 0, CAST(N'2023-02-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (4, N'Lý Nguyễn Hoàng', N'Kinh nghiệm hơn 30 năm trong điều trị da liễu, Bác sĩ Lý Nguyễn Hoàng là một trong những Bác sĩ chuyên môn cao và am hiểu sâu về da liễu và điều trị mụn', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-HOANG.png', 0, CAST(N'2022-12-14T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (5, N'Trần Ngọc Nhân', N'Kinh nghiệm 6 năm điều trị và giảng dạy về Bộ môn Da liễu, là Bác sĩ cố vấn chuyên môn, phục vụ giảng dạy cho Phòng khám, là đồng chủ biên cuốn sách “Hiểu mụn để hết mụn”', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-NHAN.png', 0, CAST(N'2022-11-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (6, N'Bùi Bảo Phương', N'Là một trong những Bác sĩ trẻ giàu kinh nghiệm, chuyên môn cao tại Phòng Khám O2 SKIN, kinh nghiệm 5 năm trong điều trị da liễu.', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-PHUONG.png', 0, CAST(N'2022-09-09T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (7, N'Lê Phượng Thúy Vi', N'Kinh nghiệm hơn 20 năm trong điều trị da liễu, là một trong những Bác sĩ đầu tiên công tác tại Phòng Khám O2 SKIN, chi nhánh Thủ đức', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-VI.png', 0, CAST(N'2022-10-03T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (8, N'Nguyễn Quỳnh Thi', N'Kinh nghiệm gần 10 năm trong lĩnh vực da liễu, là một trong những Bác sĩ đầu tiên công tác tại Phòng Khám O2 SKIN', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-TAM.png', 0, CAST(N'2023-06-24T00:00:00.000' AS DateTime))
INSERT [dbo].[Doctors] ([iddoc], [namedoc], [infordoc], [ava_doc], [hide_doc], [date_up_doc]) VALUES (9, N'Đoàn Thị Thiện Tâm', N'Là một trong những vị bác sĩ giỏi chuyên môn, giàu kinh nghiệm trong lĩnh vực da liễu, 7 năm trực tiếp khám và điều trị mụn cho hàng ngàn Khách hàng tại O2 SKIN', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/BS-THI.png', 0, CAST(N'2022-07-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([sttfb], [idbill], [cmt], [datefb], [hidefb], [iduser]) VALUES (1, 1547451, N'Sản phẩm rất tốt', CAST(N'2023-03-13T00:00:00.000' AS DateTime), 0, N'htye7go15')
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[Medias] ON 

INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (1, N'https://suckhoedoisong.vn/vi-sao-can-tri-mun-theo-phac-do-ca-nhan-hoa-169179405.htm', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/bao-suc-khoe-va-doi-song.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (2, N'https://alobacsi.com/o2-skin-cai-tien-phuong-phap-dieu-tri-mun-sau-6-thang-nghien-cuu-va-thu-nghiem-n411136.html', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/bao-alobacsi.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (3, N'https://zingnews.vn/gioi-tre-tphcm-chia-se-hieu-qua-tri-mun-tai-o2-skin-post1118072.html', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/bao-zing.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (4, N'https://thanhnien.vn/o2-skin-thuong-hieu-tri-mun-hang-dau-danh-cho-sinh-vien-185831079.htm', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/bao-thanh-niem.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (5, N'https://tuoitre.vn/o2-skin-noi-quy-tu-doi-ngu-bac-si-giau-kinh-nghiem-trong-dieu-tri-mun-20200523143020576.htm', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/bao-tuoi-tre.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (6, N'https://www.youtube.com/watch?v=T3FA417Cltw', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/kenh-htv-9.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (7, N'https://www.youtube.com/watch?v=CChqLepmTlY', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/kenh-htv-7.jpg', 0)
INSERT [dbo].[Medias] ([idmedia], [hrefmedia], [imgmedia], [hidemedia]) VALUES (8, N'https://kenh14.vn/o2-skin-phong-kham-chuyen-tri-mun-duoc-long-gioi-tre-sai-thanh-20200609120713278.chn', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/kenh-14-e1656996269656.jpg', 0)
SET IDENTITY_INSERT [dbo].[Medias] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([idp], [namep], [typep], [newprice], [oldprice], [descr], [date_create], [hide], [statep], [img]) VALUES (1, N'Kem Dưỡng Ẩm Klairs Làm Dịu & Phục Hồi Da Ban Đêm 60ml
Midnight Blue Calming Cream', 1, N'300000', N'350000', N'Kem Dưỡng Ẩm Klairs Làm Dịu & Phục Hồi Da Ban Đêm là một loại kem dưỡng dành cho da nhạy cảm đang được yêu thích đến từ thương hiệu Klairs của Hàn Quốc. Nổi bật với thành phần Guaiazulene được chiết xuất từ dầu hoa cúc tạo màu xanh dịu mát tự nhiên, giúp làm dịu da kích ứng, nhạy cảm vô cùng hiệu quả. Cộng với chiết xuất rau má cấp ẩm, làm dịu da và hỗ trợ phục hồi da mụn rất tốt.', NULL, 0, N'New', N'https://xachtaynhat.net/wp-content/uploads/2021/06/Klairs-Midnight-Blue-Calming-Cream.jpg')
INSERT [dbo].[Products] ([idp], [namep], [typep], [newprice], [oldprice], [descr], [date_create], [hide], [statep], [img]) VALUES (2, N'Kem Dưỡng Obagi Retinol 0.5% Ngăn Ngừa Lão Hoá Da 28g
Retinol 0.5 Cream', 3, N'240000', N'260000', N'Obagi 360 Retinol Cream là sản phẩm kem dưỡng da trẻ hoá, ngừa mụn nổi tiếng từ thương hiệu dược mỹ phẩm Obagi Medical. Công thức với hàm lượng Retinol 0.5% / Retinol 1% hoạt động hiệu quả trên mọi làn da, đặc biệt là da dầu, giúp cải thiện các vấn đề về da như mụn, dầu thừa, lão hoá; mang đến cho bạn làn da mịn màng và tươi trẻ.', NULL, 0, N'Sale', N'https://media.hasaki.vn/catalog/product/t/o/top_fb_ads_422207968_050722-1656991298_img_358x358_843626_fit_center.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([name_dt], [desc_dt], [hide_dt], [img_dt], [id_dt], [meta], [slider_dt]) VALUES (N'Điều trị mụn', N'Các dịch vụ điều trị mụn tại O2 SKIN tập trung điều trị chuyên sâu, lần lượt giải quyết từng vấn đề của làn da mụn như giảm viêm, giảm nhờn, giảm sừng hóa nang lông, hỗ trợ đẩy nhanh quá trình gom khô cồi mụn và lấy nhân mụn ra khỏi da, mang lại cảm giác nhẹ nhàng, sạch thoáng lỗ chân lông.', 0, N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/Dich-vu-dieu-tri-mun.png', 1, N'dieu-tri-mun', N'dtm')
INSERT [dbo].[Services] ([name_dt], [desc_dt], [hide_dt], [img_dt], [id_dt], [meta], [slider_dt]) VALUES (N'Điều trị phục hồi da', N'Sử dụng các phương pháp điều trị mụn kết hợp giúp giải quyết đồng thời các vấn đề của làn da mụn như giảm các triệu chứng sưng viêm, đỏ da, cân bằng nhờn, điều trị mụn dưới da, giảm tình trạng da dày sừng, hỗ trợ đẩy nhanh quá trình gom khô cồi mụn và rút ngắn thời gian điều trị mụn hiệu quả', 0, N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/dieu-tri-mun-ket-hop-e1637831941671.jpg', 2, N'dieu-tri-phuc-hoi-da', N'dtphd')
INSERT [dbo].[Services] ([name_dt], [desc_dt], [hide_dt], [img_dt], [id_dt], [meta], [slider_dt]) VALUES (N'Điều trị thâm', N'Da nhạy cảm, da đang bị dị ứng, kích ứng hoặc tai biến corticoid sẽ được điều trị bằng các phương pháp điện di chuyên sâu, đắp mặt nạ phục hồi, cấp ẩm tức thì, làm dịu da và giảm các triệu chứng kích ứng, đỏ da, rát da, giúp phục hồi da về trạng thái cân bằng, khỏe mạnh.', 0, N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/dieu-tri-phuc-hoi-danh-cho-da-nhay-cam-02.jpg', 3, N'dieu-tri-tham', N'dtt')
INSERT [dbo].[Services] ([name_dt], [desc_dt], [hide_dt], [img_dt], [id_dt], [meta], [slider_dt]) VALUES (N'Điều trị sẹo rỗ', N'Sau điều trị mụn thành công, da cần được phục hồi và điều trị các tổn thương sau mụn bằng các phương pháp phục hồi chuyên sâu, giúp cấp ẩm, tái tạo và phục hồi da, làm mờ vết thâm, hồng ban sau mụn, làm đều màu da, trắng sáng da, ngăn ngừa hình thành sẹo rỗ và các biến chứng khác…', 0, N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/7-Lan-kim-nong.jpg', 4, N'dieu-tri-seo-ro', N'dtsr')
INSERT [dbo].[Services] ([name_dt], [desc_dt], [hide_dt], [img_dt], [id_dt], [meta], [slider_dt]) VALUES (N'Điều trị mụn kết hợp', N'Những tổn thương sau mụn cần được hỗ trợ phục hồi càng sớm càng tốt bằng các phương pháp kỹ thuật cao, giúp điều trị chuyên sâu cho từng vấn đề như sẹo rỗ, thâm mụn, hồng ban, lỗ chân lông to, da dày sừng…Giúp làn da sớm trở lại trạng thái khỏe mạnh, mịn màng và sạch mụn.', 0, N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/4-Laser-Fractional.png', 5, N'dieu-tri-mun-ket-hop', N'dtmkh')
INSERT [dbo].[Services] ([name_dt], [desc_dt], [hide_dt], [img_dt], [id_dt], [meta], [slider_dt]) VALUES (N'Chăm sóc da sau điều trị', N'Những vết thâm do mụn để lại sẽ tồn tại rất lâu trên da nếu không có phương pháp thúc đẩy điều trị. Dịch vụ điều trị thâm, hồng ban sau mụn tại O2 SKIN dành cho mọi tuýp da và tình trạng da sẽ giúp bạn nhanh chóng xóa mờ thâm, sớm lấy lại làn da trắng sáng, đều màu và mịn màng.', 0, N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/8-Dien-di.jpg', 6, N'cham-soc-da-sau-dieu-tri', N'csdsdt')
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[ServicesDetails] ON 

INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (1, N'Chiếu ánh sáng sinh học xanh, đỏ', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/10/1-chieu-anh-sang.png', 0, N'100000', 1)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (2, N'Điện di điều trị mụn (cho da bình thường)', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/3-dien-di.png', 0, N'340000', 1)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (3, N'Điện di điều trị mun (cho da nhạy cảm)', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/12/31-csd-sau-dtri-mun-dien-di.png', 0, N'420000', 1)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (4, N'Điện di cấp ẩm, làm diệu và giảm kích ứng da', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/9-dien-di-giam-kich-ung.png', 0, N'340000', 2)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (5, N'Điện di phục hồi da tổn thương', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/12/29-csd-sau-dtri-mun-nhay-cam.png', 0, N'340000', 2)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (6, N'Điện di điều trị hồng da do mụn (cho da nhạy cảm)', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/9-dien-di-giam-kich-ung-1.png', 0, N'420000', 2)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (7, N'IPL trị mụn và kiểm soát nhờn', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/12/14-IPL-hong-ban-1.jpg', 0, N'970000', 5)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (8, N'Lăn kim trị mụn dưới da dày sừng', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2021/11/19-Lan-kim-nong.png', 0, N'1500000', 5)
INSERT [dbo].[ServicesDetails] ([id_sd], [name_sd], [img_sd], [hide_sd], [price_sd], [id_dt]) VALUES (9, N'Khám mụn chuẩn y khoa', N'https://o2skin.cdn.vccloud.vn/wp-content/uploads/2022/04/kham-mun-chuan-y-khoa.jpg', 0, N'100000', 1)
SET IDENTITY_INSERT [dbo].[ServicesDetails] OFF
GO
INSERT [dbo].[Users] ([iduser], [name], [birth], [gender], [phone], [email], [password], [hide], [block], [ava]) VALUES (N'htye7go15', N'Nguyễn Thị Anh Thư', CAST(N'2001-03-09T00:00:00.000' AS DateTime), N'Female', N'0578412547', N'anhthu@gmail.com', N'123456', 0, 0, N'https://media.viez.vn/prod/2021/11/13/1636781587505_18478cd9d6.jpeg')
INSERT [dbo].[Users] ([iduser], [name], [birth], [gender], [phone], [email], [password], [hide], [block], [ava]) VALUES (N'pouR71lnC9', N'Nguyễn Văn Huy', CAST(N'2001-02-28T00:00:00.000' AS DateTime), N'Male', N'0321574124', N'vanhuy1619@gmail.com', N'123456', 0, 0, N'https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2021/10/9/961804/Ca-Si-Lisa-Blackpink.jpg')
GO
USE [master]
GO
ALTER DATABASE [DoctorSkin] SET  READ_WRITE 
GO
