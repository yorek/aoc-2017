DECLARE @v INT = 312051;

WITH cte AS 
(
SELECT 
	l AS [level],
	1 + ((l-2) * 2) AS previous_side_length,
	1 + ((l-1) * 2) AS side_length
FROM 
	(
		SELECT TOP (1000000) ROW_NUMBER() OVER (ORDER BY a.object_id) AS l FROM sys.all_columns a CROSS APPLY sys.all_columns b	
	) AS T
),
cte3 AS
(
	SELECT
		[level],
		side_length,
		previous_side_length,
		side_length * side_length AS perimeter_length,
		previous_side_length * previous_side_length AS previous_perimeter_length,
		(previous_side_length*previous_side_length) + 1 AS start_at,
		side_length * side_length AS end_at
	FROM
		cte
), cte4 AS
(
SELECT 
	*,
	CASE WHEN side = 1 THEN start_at ELSE (previous_perimeter_length) + (side_length * (side-1)) - (side-1) END AS side_from,
	(previous_perimeter_length) + (side_length * side) - side AS side_to
FROM 
	cte3 
CROSS JOIN
	(VALUES (1), (2), (3), (4)) sq(side)
)
SELECT
	*,
	steps_to_center = ABS((side_length - 1) / 2 - (side_to - @v)) + ([level]-1)
FROM
	cte4
WHERE
	@v BETWEEN side_from AND side_to
ORDER BY 
	[level]
;





