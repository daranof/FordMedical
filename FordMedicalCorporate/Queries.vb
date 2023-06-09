Module Queries
    Public qFillOrder As String =
        <SQL><![CDATA[
			        SELECT Id, OrderNumber, Cutomer
                                       From Orders
                                       Where OrderNumber Like '%@OrderNumber%' 
		    ]]></SQL>

    Public qFillSKU As String =
        <SQL><![CDATA[
			        SELECT A.Id as Id, LTRIM(RTRIM(A.SKU)) As SKU
                                   FROM SKU as A
                                   WHERE A.Id >= 0 and @FILTER
		    ]]></SQL>


    Public qFillSKUFinder As String =
        <SQL><![CDATA[
			       Select distinct p.OrderId, k.Id, k.SKU, p.Qty, p.PalletId ,o.OrderNumber, c.Name as Customer, s.ShippingLot as Shipment from palletizer as p
                    left Join Orders as o on p.OrderId=o.Id
                    left Join Customer as c on o.CustomerId=c.Id
                    left Join Shipping as s on p.ShippingId=s.ShippingId
                    left Join Sku as k on p.SKUId=k.id
                    where k.SKU='@SKUFinder'
                    
		    ]]></SQL>

    Public qFillShipping As String =
        <SQL><![CDATA[
			       SELECT   s.ShippingId
                            ,s.Customer
                            ,s.Date
                            ,s.ShippingLot
                            ,s.Shipped
                            ,s.Measures
                            
                    FROM    Shipping as s
                                      
                    ORDER   BY s.Customer 
		    ]]></SQL>

    Public qCmbCustomer As String =
       <SQL><![CDATA[
			       SELECT Id, Name From Customer
              				  
		    ]]></SQL>

    Public qCmbShippingLot As String =
       <SQL><![CDATA[
			       SELECT ShippingId, ShippingLot, CustomerId From Shipping where CustomerId=@CustomerId
              				  
		    ]]></SQL>

    Public qCmbPallet As String =
      <SQL><![CDATA[
			       SELECT Id, PalletNumber From Pallet
              				  
		    ]]></SQL>
    Public qCmbPalletMeasures As String =
     <SQL><![CDATA[
			        SELECT distinct p.Id, p.PalletNumber From Pallet as p
                    left join Palletizer as z on p.Id=z.PalletId
                    where z.ShippingId=@ShippingId
                    order by p.Id
              				  
		    ]]></SQL>

    Public qCmbOrders As String =
      <SQL><![CDATA[
			             SELECT o.Id, o.OrderNumber
                                       From Orders as o
									   LEFT JOIN Customer as c on o.CustomerId=c.Id
									   WHERE o.ShippingId=@ShippingId and o.CustomerId=@CustomerId and @FILTER
                                     
		    ]]></SQL>

    Public qCmbOrders2 As String =
      <SQL><![CDATA[
			             SELECT o.Id, o.OrderNumber
                                       From Orders as o
									   @FILTER                                     
		    ]]></SQL>

    Public qCmbOrdersReport As String =
    <SQL><![CDATA[
			             SELECT o.Id, o.OrderNumber
                                       From Orders as o
									   LEFT JOIN Customer as c on o.CustomerId=c.Id
									   WHERE o.ShippingId=@ShippingId and o.CustomerId=@CustomerId
                                       ORDER BY o.Id
		    ]]></SQL>

    Public qCmbSKU As String =
     <SQL><![CDATA[
			             SELECT s.Id, s.Name
                                       From SKU as s
									  
                                       ORDER BY s.Id
		    ]]></SQL>

    Public qFillCustomer2 As String =
     <SQL><![CDATA[
			    DECLARE @Pallet nvarchar (500)
SET @Pallet=''

SELECT @Pallet = @Pallet + '[' +  PP.Pallet + '],' 
FROM (
    SELECT DISTINCT CAST(PalletNumber AS varchar(500)) AS Pallet 
    FROM Pallet AS t
    LEFT JOIN Palletizer AS z ON t.Id = z.PalletId
    LEFT JOIN Shipping AS s ON z.ShippingId = s.ShippingId
    WHERE s.CustomerId = @CustomerId AND s.ShippingLot = @ShippingLot
) AS PP

SET @Pallet = LEFT(@Pallet, LEN(@Pallet) - 1)

DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)
DECLARE @ColumnName AS NVARCHAR(MAX)

SELECT @ColumnName = COALESCE(@ColumnName + ',','') + QUOTENAME(PalletNumber)
FROM (
    SELECT DISTINCT PalletNumber 
    FROM Pallet AS t
    LEFT JOIN Palletizer AS z ON t.Id = z.PalletId
    LEFT JOIN Shipping AS s ON z.ShippingId = s.ShippingId
    WHERE s.CustomerId = @CustomerId AND s.ShippingLot = @ShippingLot
) AS P

SET @DynamicPivotQuery = N'
SELECT SKU, Orders, Total, ' + @ColumnName + '
FROM (
    SELECT k.SKU, 
           STUFF((SELECT DISTINCT '', '' + o.OrderNumber 
                  FROM Palletizer AS z2 
                  LEFT JOIN Shipping AS s2 ON z2.ShippingId = s2.ShippingId
                  LEFT JOIN Orders AS o ON z2.OrderId = o.Id
                  WHERE s2.CustomerId = @CustomerId AND s2.ShippingLot = @ShippingLot AND z2.SKUId = k.Id
                  FOR XML PATH('''')), 1, 2, '''') AS Orders,
           p.PalletNumber, 
           SUM(z.Qty) OVER (PARTITION BY k.SKU) AS Total, 
           z.Qty
    FROM SKU AS k
    LEFT JOIN Palletizer AS z ON k.Id = z.SKUId
    LEFT JOIN Shipping AS s ON z.ShippingId = s.ShippingId
    LEFT JOIN Pallet AS p ON z.PalletId = p.Id								
    WHERE s.CustomerId = @CustomerId AND s.ShippingLot = @ShippingLot AND z.Qty > 0
) AS R
PIVOT (
    SUM(Qty)
    FOR PalletNumber IN (' + @ColumnName + ')
) AS Piv
ORDER BY SKU'

EXEC sp_executesql @DynamicPivotQuery
		    ]]></SQL>

    Public qOrdersPalletizer As String =
      <SQL><![CDATA[
    Select Case k.SKU, 
    STUFF((SELECT DISTINCT '', '' + o.OrderNumber 
           FROM Palletizer As z2 
           LEFT JOIN Shipping As s2 On z2.ShippingId = s2.ShippingId
           LEFT JOIN Orders As o On z2.OrderId = o.Id
           WHERE s2.CustomerId = @CustomerId And s2.ShippingLot = @ShippingLot And z2.SKUId = k.Id
           For XML PATH('OrdersNumber')), 1, 2, 'Orders') AS Orders
FROM SKU As k
LEFT JOIN Palletizer As z On k.Id = z.SKUId
LEFT JOIN Shipping As s On z.ShippingId = s.ShippingId
LEFT JOIN Pallet As p On z.PalletId = p.Id								
WHERE s.CustomerId = @CustomerId And s.ShippingLot = @ShippingLot
GROUP BY k.Id, k.SKU
Order by k.SKU

      ]]></SQL>



    Public qFillCustomer1 As String =
    <SQL><![CDATA[
			   Select *
       from (SELECT  distinct p.PalletNumber, z.Qty
                    FROM    Shipping as s
					LEFT JOIN Palletizer as z on s.ShippingId=z.ShippingId
					LEFT JOIN SKU as k on z.SKUId=k.Id
					LEFT JOIN Pallet as p on z.PalletId=p.Id								
                    LEFT JOIN Invoice as i on k.Id=i.SKUId
					
					
					Group by p.PalletNumber, k.SKU, z.Qty,i.OrderId) as R
		Pivot
		( Sum (Qty)
        For PalletNumber in ("Pallet1")) as Piv
		    ]]></SQL>

    Public qFillGridMain As String =
     <SQL><![CDATA[
			      SELECT  s.ShippingId
                            ,s.CustomerId
                            ,c.Name as Customer
                            ,s.ShippingLot as Shipment
							,(SELECT count(o.Id) 
                                       From Orders as o
									   WHERE o.ShippingId=s.ShippingId) as TotalOrders
							,(SELECT count(distinct(p.OrderId)) 
                                       From Palletizer as p
									   LEFT JOIN Orders as o on p.OrderId=o.Id
									   WHERE p.ShippingId=s.ShippingId) as OrdersInPallets
                            ,max (m.PalletId) as Pallets
							,s.Measures
							,s.Shipped
                            ,s.ShippedDate
                            ,cast(s.Date as Date) as Date
                            ,s.BOL
                    FROM    Shipping as s
                    LEFT JOIN Customer as c on s.CustomerId=c.Id     
					LEFT JOIN Palletizer as m on s.ShippingId=m.ShippingId

					group by s.ShippingId, c.Name, s.ShippingLot, s.Measures, s.CustomerId, s.Date,s.Shipped , s.ShippedDate, s.BOL
                    order by s.Shipped, Customer, s.ShippingLot desc
		    ]]></SQL>

    Public qFillShipped As String =
   <SQL><![CDATA[
			      SELECT  s.ShippingId
                            ,c.Name
                            ,s.Date
                            ,s.ShippingLot
                            ,s.Shipped
                            ,s.Measures
                            
                    FROM    Shipping as s
                    LEFT JOIN Customer as c on s.CustomerId=c.Id      
                    Left join Palletizer
                    ORDER   BY s.ShippingId
		    ]]></SQL>

    Public qTotalWeight As String =
 <SQL><![CDATA[
			      select sum (m.Weight) from Measure as m
                                        Left Join Shipping as s on m.IdShipping=s.ShippingId
                                        where s.CustomerId=@CustomerId and s.ShippingLot=@ShippingLot
		    ]]></SQL>

    Public qFillGridPalletizer As String =
    <SQL><![CDATA[
			      Select p.Id, z.PalletID, p.PalletNumber, z.SKUId, s.SKU, z.Qty, z.ShippingId, z.PalletizerId, z.OrderId, o.OrderNumber
                               From palletizer as z
                               Left join Pallet as p on p.Id=z.PalletId
                               Left join SKU as s on z.SKUId=s.Id
							   Left join Orders as o on z.OrderId=o.Id
                              
                               Where z.SKUId=@SKUId and z.ShippingId=@ShippingId
                               Order by p.PalletNumber
		    ]]></SQL>

    Public qFillGridPalletizerOrder As String =
   <SQL><![CDATA[
			        Select p.Id, z.PalletID, p.PalletNumber, z.SKUId, s.SKU, z.Qty, z.ShippingId, z.PalletizerId, z.OrderId, o.OrderNumber
                               From palletizer As z
                               Left join Pallet As p On p.Id=z.PalletId
                               Left join SKU As s On z.SKUId=s.Id
							   Left join Orders As o On z.OrderId=o.Id
                              
                               Where z.SKUId=@SKUId And z.ShippingId=@ShippingId and z.OrderId=@OrderId
                               Order by p.PalletNumber
		    ]]></SQL>

    Public qSKU As String =
  <SQL><![CDATA[
			      select s.Id from SKU as s
                  right join Invoice as i on s.Id=i.SKUId
                              
                               Where s.Id=@SKUId
                               Order by s.Id
		    ]]></SQL>

    Public qOrder As String =
 <SQL><![CDATA[
			      select o.Id from Orders as o
                  right join Invoice as i on o.Id=i.OrderId
                              
                               Where o.Id=@OrderId
                               Order by o.Id
		    ]]></SQL>

    Public qFillColumnas As String =
   <SQL><![CDATA[
                    Select distinct cast (PalletNumber As varchar(300)) As Pallet from Pallet As t
                              Left Join Palletizer As z On t.Id=z.PalletId
                              Left Join Shipping As s On z.ShippingId=s.ShippingId
                              WHERE(s.CustomerId =@CustomerId And s.ShippingLot=@ShippingLot)
    ]]></SQL>

    Public qFillMeasure As String =
 <SQL><![CDATA[
                   Select m.IdShipping, p.PalletNumber, m.Long, m.Width, m.Hight, m.Weight from measure as m
		                    left join Pallet as p on m.IdPallet=p.Id
		                    where m.IdShipping=@ShippingId
    ]]></SQL>

    Public qFillMeasureReport As String =
<SQL><![CDATA[
                  SELECT p.PalletNumber, m.Long, m.Width, m.Hight, m.Weight
                                       From Measure as m
                                       Left Join Pallet as p on m.IdPallet=p.id
									   left Join Shipping as s on s.ShippingId=m.IdShipping
                                       where s.CustomerId =@CustomerId And s.ShippingLot=@ShippingLot
                                       Order by p.PalletNumber
    ]]></SQL>

End Module
