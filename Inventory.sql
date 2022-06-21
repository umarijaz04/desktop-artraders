USE [master]
GO
/****** Object:  Database [Inventory]    Script Date: 20-Mar-21 2:31:58 PM ******/
CREATE DATABASE [Inventory]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'veronati_hassan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\veronati_hassan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'veronati_hassan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\veronati_hassan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Inventory] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Inventory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Inventory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Inventory] SET ARITHABORT OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Inventory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Inventory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Inventory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Inventory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Inventory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Inventory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Inventory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Inventory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Inventory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Inventory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Inventory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Inventory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Inventory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Inventory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Inventory] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Inventory] SET  MULTI_USER 
GO
ALTER DATABASE [Inventory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Inventory] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Inventory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Inventory] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Inventory] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Inventory] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Inventory] SET QUERY_STORE = OFF
GO
USE [Inventory]
GO
/****** Object:  Schema [veronati_admin]    Script Date: 20-Mar-21 2:31:58 PM ******/
CREATE SCHEMA [veronati_admin]
GO
/****** Object:  View [dbo].[avail_qty_sb_temp]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_sb_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) SB_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) SB_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size
GO
/****** Object:  View [dbo].[avail_qty_sb]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_sb] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY sb.P_Code, sb.Size) sr,sb.P_Code,sb.Tone,sb.Quality,sb.Size,sb.SB_Qty, sb.SB_Tiles, CAST(ROUND((p.meters*(sb.SB_Qty + (sb.SB_Tiles / p.pieces))),2) as decimal(18,2)) SB_Mtrs from avail_qty_sb_temp sb ,product p where sb.p_Code=p.p_id AND sb.Size=p.size
GO
/****** Object:  View [dbo].[avail_qty_st_temp]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_st_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) ST_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) ST_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size
GO
/****** Object:  View [dbo].[avail_qty_st]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_st] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY st.P_Code, st.Size) sr,st.P_Code,st.Tone,st.Quality,st.Size,st.ST_Qty,st.ST_Tiles,CAST(ROUND((p.meters*(st.ST_Qty + (st.ST_Tiles / p.pieces))),2) as decimal(18,2)) ST_Mtrs from avail_qty_st_temp st ,product p where st.p_Code=p.p_id AND st.Size=p.size
GO
/****** Object:  View [dbo].[avail_qty_cm_temp]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_cm_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) CM_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) CM_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size
GO
/****** Object:  View [dbo].[avail_qty_cm]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_cm] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY cm.P_Code, cm.Size) sr,cm.P_Code,cm.Tone,cm.Quality,cm.Size,cm.CM_Qty,cm.CM_Tiles,CAST(ROUND((p.meters*(cm.CM_Qty + (cm.CM_Tiles / p.pieces))),2) as decimal(18,2)) CM_Mtrs from avail_qty_cm_temp cm ,product p where cm.p_Code=p.p_id AND cm.Size=p.size 
GO
/****** Object:  View [dbo].[avail_qty_pj_temp]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_pj_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) PJ_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) PJ_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size
GO
/****** Object:  View [dbo].[avail_qty_pj]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[avail_qty_pj] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY pj.P_Code, pj.Size) sr,pj.P_Code,pj.Tone,pj.Quality,pj.Size,pj.PJ_Qty,pj.PJ_Tiles,CAST(ROUND((p.meters*(pj.PJ_Qty + (pj.PJ_Tiles / p.pieces))),2) as decimal(18,2)) PJ_Mtrs from avail_qty_pj_temp pj ,product p where pj.p_Code=p.p_id AND pj.Size=p.size 
GO
/****** Object:  View [dbo].[stock]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create VIEW [dbo].[stock] AS SELECT DISTINCT sb.P_Code,sb.Tone,sb.Size,sb.SB_Qty,sb.SB_Tiles,sb.SB_Mtrs,st.ST_Qty,st.ST_Tiles,st.ST_Mtrs,cm.CM_Qty,cm.CM_Tiles,cm.CM_Mtrs,pj.PJ_Qty,pj.PJ_Tiles,pj.PJ_Mtrs from avail_qty_sb sb ,avail_qty_st st,avail_qty_cm cm,avail_qty_pj pj where sb.sr=st.sr AND sb.sr=cm.sr AND sb.sr=pj.sr
GO
/****** Object:  Table [dbo].[breakage_cart_s]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[breakage_cart_s](
	[P_Code] [varchar](50) NULL,
	[P_Name] [varchar](50) NULL,
	[Quantity] [decimal](18, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[breakage_detail2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[breakage_detail2](
	[b_id] [decimal](18, 0) NOT NULL,
	[p_id] [varchar](50) NOT NULL,
	[qty] [decimal](18, 0) NULL,
 CONSTRAINT [PK_breakage_detail2] PRIMARY KEY CLUSTERED 
(
	[b_id] ASC,
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[breakage_stock]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[breakage_stock](
	[b_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[ps_id] [decimal](18, 0) NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_breakage_stock] PRIMARY KEY CLUSTERED 
(
	[b_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart_s]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart_s](
	[P_Code] [varchar](50) NULL,
	[P_Name] [varchar](50) NULL,
	[Quantity] [decimal](18, 0) NULL,
	[Price] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cash_transaction]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cash_transaction](
	[cat_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_cash_transaction] PRIMARY KEY CLUSTERED 
(
	[cat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cheque]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cheque](
	[ch_no] [decimal](18, 0) NOT NULL,
	[c_id] [decimal](18, 0) NULL,
	[bank_name] [varchar](50) NULL,
	[amount] [decimal](18, 0) NULL,
	[status] [varchar](50) NULL,
	[ss_id] [decimal](18, 0) NULL,
	[checked] [varchar](50) NULL,
	[cat_id] [decimal](18, 0) NULL,
 CONSTRAINT [PK_cheque] PRIMARY KEY CLUSTERED 
(
	[ch_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cus_transaction]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cus_transaction](
	[cus_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_cus_transaction] PRIMARY KEY CLUSTERED 
(
	[cus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[c_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[c_name] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[contact] [varchar](50) NULL,
	[type] [varchar](50) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[c_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer_transaction]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer_transaction](
	[ct_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[c_id] [decimal](18, 0) NULL,
	[amount] [decimal](18, 2) NULL,
	[datetime] [datetime] NULL,
	[status] [varchar](50) NULL,
	[cat_id] [decimal](18, 0) NULL,
	[cus_id] [decimal](18, 0) NULL,
	[description] [varchar](100) NULL,
	[type] [varchar](50) NULL,
 CONSTRAINT [PK_customer_transaction] PRIMARY KEY CLUSTERED 
(
	[ct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[expense]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[expense](
	[e_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NULL,
	[amount] [decimal](18, 2) NULL,
	[datetime] [datetime] NULL,
	[cat_id] [decimal](18, 0) NULL,
	[type] [varchar](50) NULL,
 CONSTRAINT [PK_expense] PRIMARY KEY CLUSTERED 
(
	[e_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[limit]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[limit](
	[id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[qty] [decimal](18, 0) NULL,
 CONSTRAINT [PK_limit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[man_transaction]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[man_transaction](
	[man_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_man_transaction] PRIMARY KEY CLUSTERED 
(
	[man_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[manu_transaction]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[manu_transaction](
	[mt_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[m_id] [decimal](18, 0) NULL,
	[amount] [decimal](18, 2) NULL,
	[datetime] [datetime] NULL,
	[status] [varchar](50) NULL,
	[cat_id] [decimal](18, 0) NULL,
	[man_id] [decimal](18, 0) NULL,
	[description] [varchar](100) NULL,
	[type] [varchar](50) NULL,
 CONSTRAINT [PK_manu_transaction] PRIMARY KEY CLUSTERED 
(
	[mt_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[manufacturer]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[manufacturer](
	[m_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[contact] [decimal](18, 0) NULL,
 CONSTRAINT [PK_manufacturer] PRIMARY KEY CLUSTERED 
(
	[m_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[office_account]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[office_account](
	[id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[fname] [varchar](50) NULL,
	[uname] [varchar](50) NULL,
	[pass] [varchar](50) NULL,
 CONSTRAINT [PK_office_account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_cart_s]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_cart_s](
	[P_Code] [varchar](50) NULL,
	[P_Name] [varchar](50) NULL,
	[Quantity] [decimal](18, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_detail2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail2](
	[o_id] [decimal](18, 0) NOT NULL,
	[p_id] [varchar](50) NOT NULL,
	[qty] [decimal](18, 0) NULL,
 CONSTRAINT [PK_order_detail2] PRIMARY KEY CLUSTERED 
(
	[o_id] ASC,
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_stock]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_stock](
	[o_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[c_id] [decimal](18, 0) NULL,
	[datetime] [datetime] NULL,
	[m_id] [decimal](18, 0) NULL,
	[status] [varchar](50) NULL,
 CONSTRAINT [PK_order_stock] PRIMARY KEY CLUSTERED 
(
	[o_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pr_cart_s]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pr_cart_s](
	[P_Code] [varchar](50) NULL,
	[P_Name] [varchar](50) NULL,
	[Quantity] [decimal](18, 0) NULL,
	[Price] [decimal](18, 2) NULL,
	[Deduct] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[prebalance]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prebalance](
	[name] [varchar](50) NULL,
	[balance] [decimal](18, 2) NULL,
	[contact] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product2](
	[p_id] [varchar](50) NOT NULL,
	[p_name] [varchar](50) NULL,
	[category] [varchar](50) NULL,
 CONSTRAINT [PK_product2] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase_cart_s]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase_cart_s](
	[P_Code] [varchar](50) NULL,
	[P_Name] [varchar](50) NULL,
	[Quantity] [decimal](18, 0) NULL,
	[Price] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase_return]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase_return](
	[pr_id] [decimal](18, 0) NOT NULL,
	[c_id] [decimal](18, 0) NULL,
	[m_id] [decimal](18, 0) NULL,
	[cus_id] [decimal](18, 0) NULL,
	[man_id] [decimal](18, 0) NULL,
	[cat_id] [decimal](18, 0) NULL,
	[total] [decimal](18, 2) NULL,
	[receive] [decimal](18, 2) NULL,
	[datetime] [datetime] NULL,
	[ps_id] [decimal](18, 0) NULL,
 CONSTRAINT [PK_purchase_return] PRIMARY KEY CLUSTERED 
(
	[pr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase_return_detail2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase_return_detail2](
	[pr_id] [decimal](18, 0) NOT NULL,
	[p_id] [varchar](50) NOT NULL,
	[qty] [decimal](18, 0) NULL,
	[deduct] [decimal](18, 2) NULL,
	[price] [decimal](18, 2) NULL,
 CONSTRAINT [PK_purchase_return_detail2] PRIMARY KEY CLUSTERED 
(
	[pr_id] ASC,
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase_stock]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase_stock](
	[datetime] [datetime] NULL,
	[m_id] [decimal](18, 0) NULL,
	[total] [decimal](18, 2) NULL,
	[paid] [decimal](18, 2) NULL,
	[cat_id] [decimal](18, 0) NULL,
	[man_id] [decimal](18, 0) NULL,
	[ps_id] [decimal](18, 0) NOT NULL,
	[pending] [varchar](50) NULL,
	[c_id] [decimal](18, 0) NULL,
	[cus_id] [decimal](18, 0) NULL,
 CONSTRAINT [PK_purchase_stock] PRIMARY KEY CLUSTERED 
(
	[ps_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase_stock_detail2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase_stock_detail2](
	[ps_id] [decimal](18, 0) NOT NULL,
	[p_id] [varchar](50) NOT NULL,
	[qty] [decimal](18, 0) NULL,
	[pprice] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_purchase_stock_detail2] PRIMARY KEY CLUSTERED 
(
	[ps_id] ASC,
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sale_return]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale_return](
	[ss_id] [decimal](18, 0) NULL,
	[total] [decimal](18, 2) NULL,
	[deduct] [decimal](18, 2) NULL,
	[Paid] [decimal](18, 2) NULL,
	[datetime] [datetime] NULL,
	[cat_id] [decimal](18, 0) NULL,
	[c_id] [decimal](18, 0) NULL,
	[cus_id] [decimal](18, 0) NULL,
	[sr_id] [decimal](18, 0) NOT NULL,
	[m_id] [decimal](18, 0) NULL,
	[man_id] [decimal](18, 0) NULL,
 CONSTRAINT [PK_sale_return] PRIMARY KEY CLUSTERED 
(
	[sr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sale_return_detail2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale_return_detail2](
	[sr_id] [decimal](18, 0) NOT NULL,
	[p_id] [varchar](50) NOT NULL,
	[qty] [decimal](18, 0) NULL,
	[deduct] [decimal](18, 2) NULL,
	[price] [decimal](18, 2) NULL,
 CONSTRAINT [PK_sale_return_detail2] PRIMARY KEY CLUSTERED 
(
	[sr_id] ASC,
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sale_stock]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale_stock](
	[c_id] [decimal](18, 0) NULL,
	[datetime] [datetime] NULL,
	[total] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[receive] [decimal](18, 2) NULL,
	[cat_id] [decimal](18, 0) NULL,
	[cus_id] [decimal](18, 0) NULL,
	[ss_id] [decimal](18, 0) NOT NULL,
	[man_id] [decimal](18, 0) NULL,
	[m_id] [decimal](18, 0) NULL,
	[sub_id] [decimal](18, 0) NULL,
 CONSTRAINT [PK_sale_stock] PRIMARY KEY CLUSTERED 
(
	[ss_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sale_stock_detail2]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale_stock_detail2](
	[ss_id] [decimal](18, 0) NOT NULL,
	[p_id] [varchar](50) NOT NULL,
	[price] [decimal](18, 2) NULL,
	[qty] [decimal](18, 0) NULL,
	[discount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_sale_stock_detail2] PRIMARY KEY CLUSTERED 
(
	[ss_id] ASC,
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sr_cart_s]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sr_cart_s](
	[P_Code] [varchar](50) NULL,
	[P_Name] [varchar](50) NULL,
	[Quantity] [decimal](18, 0) NULL,
	[Price] [decimal](18, 2) NULL,
	[Deduct] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sub_account]    Script Date: 20-Mar-21 2:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sub_account](
	[sub_id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[c_id] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_sub_account] PRIMARY KEY CLUSTERED 
(
	[sub_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[limit] ON 

INSERT [dbo].[limit] ([id], [qty]) VALUES (CAST(1 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[limit] OFF
GO
SET IDENTITY_INSERT [dbo].[office_account] ON 

INSERT [dbo].[office_account] ([id], [fname], [uname], [pass]) VALUES (CAST(1 AS Decimal(18, 0)), N'Admin', N'admin', N'21232f297a57a5a743894a0e4a801fc3')
INSERT [dbo].[office_account] ([id], [fname], [uname], [pass]) VALUES (CAST(2 AS Decimal(18, 0)), N'Imran', N'imran', N'e18fdc9fa7cc2b5f4e497d21a48ea3b7')
SET IDENTITY_INSERT [dbo].[office_account] OFF
GO
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'DUTY SANITARY CHINA', CAST(2508775.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'SADQA/KHARAIT', CAST(10150.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'PETROL', CAST(7000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'WASEEM BHAI', CAST(75000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'KHANA SHOWROOM', CAST(41576.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'LOAD HASSAN', CAST(7390.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'HASSAN KHARCHA', CAST(72500.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'GUARD PAY', CAST(6000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'SHOWROOM RENT', CAST(75000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'SAFAI WALA ', CAST(1500.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'BILL SHOWROOM', CAST(11588.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[prebalance] ([name], [balance], [contact]) VALUES (N'GHAR KHARCHA', CAST(10436.00 AS Decimal(18, 2)), NULL)
GO
ALTER TABLE [dbo].[cart_s] ADD  CONSTRAINT [DF_cart_s_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[pr_cart_s] ADD  CONSTRAINT [DF_pr_cart_s_Deduct]  DEFAULT ((0)) FOR [Deduct]
GO
ALTER TABLE [dbo].[purchase_cart_s] ADD  CONSTRAINT [DF_purchase_cart_s_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[sale_return_detail2] ADD  CONSTRAINT [DF_sale_return_detail2_deduct]  DEFAULT ((0)) FOR [deduct]
GO
ALTER TABLE [dbo].[sr_cart_s] ADD  CONSTRAINT [DF_sr_cart_s_Deduct]  DEFAULT ((0)) FOR [Deduct]
GO
ALTER TABLE [dbo].[breakage_detail2]  WITH CHECK ADD  CONSTRAINT [FK_breakage_detail2_breakage_stock] FOREIGN KEY([b_id])
REFERENCES [dbo].[breakage_stock] ([b_id])
GO
ALTER TABLE [dbo].[breakage_detail2] CHECK CONSTRAINT [FK_breakage_detail2_breakage_stock]
GO
ALTER TABLE [dbo].[breakage_detail2]  WITH CHECK ADD  CONSTRAINT [FK_breakage_detail2_product2] FOREIGN KEY([p_id])
REFERENCES [dbo].[product2] ([p_id])
GO
ALTER TABLE [dbo].[breakage_detail2] CHECK CONSTRAINT [FK_breakage_detail2_product2]
GO
ALTER TABLE [dbo].[breakage_stock]  WITH CHECK ADD  CONSTRAINT [FK_breakage_stock_breakage_stock] FOREIGN KEY([ps_id])
REFERENCES [dbo].[purchase_stock] ([ps_id])
GO
ALTER TABLE [dbo].[breakage_stock] CHECK CONSTRAINT [FK_breakage_stock_breakage_stock]
GO
ALTER TABLE [dbo].[cheque]  WITH CHECK ADD  CONSTRAINT [FK_cheque_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[cheque] CHECK CONSTRAINT [FK_cheque_cash_transaction]
GO
ALTER TABLE [dbo].[cheque]  WITH CHECK ADD  CONSTRAINT [FK_cheque_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[cheque] CHECK CONSTRAINT [FK_cheque_customer]
GO
ALTER TABLE [dbo].[cheque]  WITH CHECK ADD  CONSTRAINT [FK_cheque_sale_stock] FOREIGN KEY([ss_id])
REFERENCES [dbo].[sale_stock] ([ss_id])
GO
ALTER TABLE [dbo].[cheque] CHECK CONSTRAINT [FK_cheque_sale_stock]
GO
ALTER TABLE [dbo].[customer_transaction]  WITH CHECK ADD  CONSTRAINT [FK_customer_transaction_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[customer_transaction] CHECK CONSTRAINT [FK_customer_transaction_cash_transaction]
GO
ALTER TABLE [dbo].[customer_transaction]  WITH CHECK ADD  CONSTRAINT [FK_customer_transaction_cus_transaction] FOREIGN KEY([cus_id])
REFERENCES [dbo].[cus_transaction] ([cus_id])
GO
ALTER TABLE [dbo].[customer_transaction] CHECK CONSTRAINT [FK_customer_transaction_cus_transaction]
GO
ALTER TABLE [dbo].[customer_transaction]  WITH CHECK ADD  CONSTRAINT [FK_customer_transaction_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[customer_transaction] CHECK CONSTRAINT [FK_customer_transaction_customer]
GO
ALTER TABLE [dbo].[expense]  WITH CHECK ADD  CONSTRAINT [FK_expense_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[expense] CHECK CONSTRAINT [FK_expense_cash_transaction]
GO
ALTER TABLE [dbo].[manu_transaction]  WITH CHECK ADD  CONSTRAINT [FK_manu_transaction_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[manu_transaction] CHECK CONSTRAINT [FK_manu_transaction_cash_transaction]
GO
ALTER TABLE [dbo].[manu_transaction]  WITH CHECK ADD  CONSTRAINT [FK_manu_transaction_man_transaction] FOREIGN KEY([man_id])
REFERENCES [dbo].[man_transaction] ([man_id])
GO
ALTER TABLE [dbo].[manu_transaction] CHECK CONSTRAINT [FK_manu_transaction_man_transaction]
GO
ALTER TABLE [dbo].[manu_transaction]  WITH CHECK ADD  CONSTRAINT [FK_manu_transaction_manu_transaction] FOREIGN KEY([m_id])
REFERENCES [dbo].[manufacturer] ([m_id])
GO
ALTER TABLE [dbo].[manu_transaction] CHECK CONSTRAINT [FK_manu_transaction_manu_transaction]
GO
ALTER TABLE [dbo].[order_detail2]  WITH CHECK ADD  CONSTRAINT [FK_order_detail2_order_stock] FOREIGN KEY([o_id])
REFERENCES [dbo].[order_stock] ([o_id])
GO
ALTER TABLE [dbo].[order_detail2] CHECK CONSTRAINT [FK_order_detail2_order_stock]
GO
ALTER TABLE [dbo].[order_detail2]  WITH CHECK ADD  CONSTRAINT [FK_order_detail2_product2] FOREIGN KEY([p_id])
REFERENCES [dbo].[product2] ([p_id])
GO
ALTER TABLE [dbo].[order_detail2] CHECK CONSTRAINT [FK_order_detail2_product2]
GO
ALTER TABLE [dbo].[order_stock]  WITH CHECK ADD  CONSTRAINT [FK_order_stock_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[order_stock] CHECK CONSTRAINT [FK_order_stock_customer]
GO
ALTER TABLE [dbo].[order_stock]  WITH CHECK ADD  CONSTRAINT [FK_order_stock_manufacturer] FOREIGN KEY([m_id])
REFERENCES [dbo].[manufacturer] ([m_id])
GO
ALTER TABLE [dbo].[order_stock] CHECK CONSTRAINT [FK_order_stock_manufacturer]
GO
ALTER TABLE [dbo].[purchase_return]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[purchase_return] CHECK CONSTRAINT [FK_purchase_return_cash_transaction]
GO
ALTER TABLE [dbo].[purchase_return]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_cus_transaction] FOREIGN KEY([cus_id])
REFERENCES [dbo].[cus_transaction] ([cus_id])
GO
ALTER TABLE [dbo].[purchase_return] CHECK CONSTRAINT [FK_purchase_return_cus_transaction]
GO
ALTER TABLE [dbo].[purchase_return]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[purchase_return] CHECK CONSTRAINT [FK_purchase_return_customer]
GO
ALTER TABLE [dbo].[purchase_return]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_man_transaction] FOREIGN KEY([man_id])
REFERENCES [dbo].[man_transaction] ([man_id])
GO
ALTER TABLE [dbo].[purchase_return] CHECK CONSTRAINT [FK_purchase_return_man_transaction]
GO
ALTER TABLE [dbo].[purchase_return]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_manufacturer] FOREIGN KEY([m_id])
REFERENCES [dbo].[manufacturer] ([m_id])
GO
ALTER TABLE [dbo].[purchase_return] CHECK CONSTRAINT [FK_purchase_return_manufacturer]
GO
ALTER TABLE [dbo].[purchase_return_detail2]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_detail2_product2] FOREIGN KEY([p_id])
REFERENCES [dbo].[product2] ([p_id])
GO
ALTER TABLE [dbo].[purchase_return_detail2] CHECK CONSTRAINT [FK_purchase_return_detail2_product2]
GO
ALTER TABLE [dbo].[purchase_return_detail2]  WITH CHECK ADD  CONSTRAINT [FK_purchase_return_detail2_purchase_return] FOREIGN KEY([pr_id])
REFERENCES [dbo].[purchase_return] ([pr_id])
GO
ALTER TABLE [dbo].[purchase_return_detail2] CHECK CONSTRAINT [FK_purchase_return_detail2_purchase_return]
GO
ALTER TABLE [dbo].[purchase_stock]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[purchase_stock] CHECK CONSTRAINT [FK_purchase_stock_cash_transaction]
GO
ALTER TABLE [dbo].[purchase_stock]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_cus_transaction] FOREIGN KEY([cus_id])
REFERENCES [dbo].[cus_transaction] ([cus_id])
GO
ALTER TABLE [dbo].[purchase_stock] CHECK CONSTRAINT [FK_purchase_stock_cus_transaction]
GO
ALTER TABLE [dbo].[purchase_stock]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[purchase_stock] CHECK CONSTRAINT [FK_purchase_stock_customer]
GO
ALTER TABLE [dbo].[purchase_stock]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_man_transaction] FOREIGN KEY([man_id])
REFERENCES [dbo].[man_transaction] ([man_id])
GO
ALTER TABLE [dbo].[purchase_stock] CHECK CONSTRAINT [FK_purchase_stock_man_transaction]
GO
ALTER TABLE [dbo].[purchase_stock]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_manufacturer] FOREIGN KEY([m_id])
REFERENCES [dbo].[manufacturer] ([m_id])
GO
ALTER TABLE [dbo].[purchase_stock] CHECK CONSTRAINT [FK_purchase_stock_manufacturer]
GO
ALTER TABLE [dbo].[purchase_stock_detail2]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_detail2_product2] FOREIGN KEY([p_id])
REFERENCES [dbo].[product2] ([p_id])
GO
ALTER TABLE [dbo].[purchase_stock_detail2] CHECK CONSTRAINT [FK_purchase_stock_detail2_product2]
GO
ALTER TABLE [dbo].[purchase_stock_detail2]  WITH CHECK ADD  CONSTRAINT [FK_purchase_stock_detail2_purchase_stock] FOREIGN KEY([ps_id])
REFERENCES [dbo].[purchase_stock] ([ps_id])
GO
ALTER TABLE [dbo].[purchase_stock_detail2] CHECK CONSTRAINT [FK_purchase_stock_detail2_purchase_stock]
GO
ALTER TABLE [dbo].[sale_return]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_cash_transaction] FOREIGN KEY([cat_id])
REFERENCES [dbo].[cash_transaction] ([cat_id])
GO
ALTER TABLE [dbo].[sale_return] CHECK CONSTRAINT [FK_sale_return_cash_transaction]
GO
ALTER TABLE [dbo].[sale_return]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_cus_transaction] FOREIGN KEY([cus_id])
REFERENCES [dbo].[cus_transaction] ([cus_id])
GO
ALTER TABLE [dbo].[sale_return] CHECK CONSTRAINT [FK_sale_return_cus_transaction]
GO
ALTER TABLE [dbo].[sale_return]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[sale_return] CHECK CONSTRAINT [FK_sale_return_customer]
GO
ALTER TABLE [dbo].[sale_return]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_man_transaction] FOREIGN KEY([man_id])
REFERENCES [dbo].[man_transaction] ([man_id])
GO
ALTER TABLE [dbo].[sale_return] CHECK CONSTRAINT [FK_sale_return_man_transaction]
GO
ALTER TABLE [dbo].[sale_return]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_manufacturer] FOREIGN KEY([m_id])
REFERENCES [dbo].[manufacturer] ([m_id])
GO
ALTER TABLE [dbo].[sale_return] CHECK CONSTRAINT [FK_sale_return_manufacturer]
GO
ALTER TABLE [dbo].[sale_return]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_sale_stock] FOREIGN KEY([ss_id])
REFERENCES [dbo].[sale_stock] ([ss_id])
GO
ALTER TABLE [dbo].[sale_return] CHECK CONSTRAINT [FK_sale_return_sale_stock]
GO
ALTER TABLE [dbo].[sale_return_detail2]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_detail2_product2] FOREIGN KEY([p_id])
REFERENCES [dbo].[product2] ([p_id])
GO
ALTER TABLE [dbo].[sale_return_detail2] CHECK CONSTRAINT [FK_sale_return_detail2_product2]
GO
ALTER TABLE [dbo].[sale_return_detail2]  WITH CHECK ADD  CONSTRAINT [FK_sale_return_detail2_sale_return] FOREIGN KEY([sr_id])
REFERENCES [dbo].[sale_return] ([sr_id])
GO
ALTER TABLE [dbo].[sale_return_detail2] CHECK CONSTRAINT [FK_sale_return_detail2_sale_return]
GO
ALTER TABLE [dbo].[sale_stock_detail2]  WITH CHECK ADD  CONSTRAINT [FK_sale_stock_detail2_product2] FOREIGN KEY([p_id])
REFERENCES [dbo].[product2] ([p_id])
GO
ALTER TABLE [dbo].[sale_stock_detail2] CHECK CONSTRAINT [FK_sale_stock_detail2_product2]
GO
ALTER TABLE [dbo].[sale_stock_detail2]  WITH CHECK ADD  CONSTRAINT [FK_sale_stock_detail2_sale_stock] FOREIGN KEY([ss_id])
REFERENCES [dbo].[sale_stock] ([ss_id])
GO
ALTER TABLE [dbo].[sale_stock_detail2] CHECK CONSTRAINT [FK_sale_stock_detail2_sale_stock]
GO
ALTER TABLE [dbo].[sub_account]  WITH CHECK ADD  CONSTRAINT [FK_sub_account_customer] FOREIGN KEY([c_id])
REFERENCES [dbo].[customer] ([c_id])
GO
ALTER TABLE [dbo].[sub_account] CHECK CONSTRAINT [FK_sub_account_customer]
GO
USE [master]
GO
ALTER DATABASE [Inventory] SET  READ_WRITE 
GO
