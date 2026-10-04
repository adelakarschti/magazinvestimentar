SELECT        TOP (5) p.stil AS stil_produs, p.tip AS nume_produs, SUM(df.cantitate) AS cantitate_totala
FROM            detalii_factura AS df INNER JOIN
                         produse AS p ON df.idProdus = p.idProdus
GROUP BY p.stil, p.tip
ORDER BY cantitate_totala DESC