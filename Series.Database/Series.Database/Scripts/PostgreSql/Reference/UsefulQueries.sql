SELECT
    child."KY_ID",
    child."TX_DESCRIPTION",
    hierarchy."FK_PARENT_SERIES"
FROM "tblHierarchy" AS hierarchy
INNER JOIN "tblSeries" AS child
    ON child."KY_ID" = hierarchy."FK_SERIES"
ORDER BY
    hierarchy."FK_PARENT_SERIES",
    child."TX_DESCRIPTION";

SELECT DISTINCT
    LEFT("TX_PERIOD", 4)::INTEGER AS "Year"
FROM "tblObservations"
WHERE "FK_SERIES" = 4
ORDER BY "Year";

SELECT
    observation."TX_PERIOD",
    observation."QY_SALES",
    observation."QY_DEMAND",
    observation."QY_SUPPLY",
    locked_sales."TX_OPINION" IS NOT NULL AS "LockedSales",
    locked_demand."TX_OPINION" IS NOT NULL AS "LockedDemand",
    locked_supply."TX_OPINION" IS NOT NULL AS "LockedSupply"
FROM "tblObservations" AS observation
LEFT JOIN "tblLockedPeriods" AS locked_sales
    ON locked_sales."FK_SERIES" = observation."FK_SERIES"
   AND locked_sales."TX_PERIOD" = observation."TX_PERIOD"
   AND locked_sales."TX_OPINION" = 'QY_SALES'
LEFT JOIN "tblLockedPeriods" AS locked_demand
    ON locked_demand."FK_SERIES" = observation."FK_SERIES"
   AND locked_demand."TX_PERIOD" = observation."TX_PERIOD"
   AND locked_demand."TX_OPINION" = 'QY_DEMAND'
LEFT JOIN "tblLockedPeriods" AS locked_supply
    ON locked_supply."FK_SERIES" = observation."FK_SERIES"
   AND locked_supply."TX_PERIOD" = observation."TX_PERIOD"
   AND locked_supply."TX_OPINION" = 'QY_SUPPLY'
WHERE observation."FK_SERIES" = 4
  AND LEFT(observation."TX_PERIOD", 4)::INTEGER = 2025
ORDER BY observation."TX_PERIOD";
