<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/aircraft">
    <html>
		<head>
			<title>Checklist</title> 
			<link href="checklist.css" rel="stylesheet" type="text/css" />
		</head>
		<body>
			<div class="checklistStyle" >
				<div class="checlistTitleStyle"><xsl:value-of select="@icao"/> checklist</div>
				<xsl:apply-templates select="phase">
				</xsl:apply-templates><br/>
				<div class="phaseStyle">
				<div class="phaseTitleStyle">Reference speeds</div>
					<table class="phaseTableStyle">
						<tr>
							<td class="itemValueStyle">Vr (rotate speed)</td><td class="itemDescStyle"><xsl:value-of select="@Vr"/></td>
						</tr>
						<tr>
							<td class="itemValueStyle">Vapp (approach speed)</td><td class="itemDescStyle"><xsl:value-of select="@Vapp"/></td>
						</tr>
						<tr>
							<td class="itemValueStyle">Vf0 (flap estension speed)</td><td class="itemDescStyle"><xsl:value-of select="@Vf0"/></td>
						</tr>
						<tr>
							<td class="itemValueStyle">Vldg (landing speed)</td><td class="itemDescStyle"><xsl:value-of select="@Vldg"/></td>
						</tr>
						<tr>
							<td class="itemValueStyle">Vne (never exceed speed)</td><td class="itemDescStyle"><xsl:value-of select="@Vne"/></td>
						</tr>
						<tr>
							<td class="itemValueStyle">Vs (stall speed)</td><td class="itemDescStyle"><xsl:value-of select="@Vs"/></td>
						</tr>
					</table>
				</div>
			</div>
		</body>
    </html>
  </xsl:template>

  <xsl:template match="phase">
    <div class="phaseStyle">
		<div class="phaseTitleStyle"><xsl:value-of select="@desc"/> checklist</div>
		<table class="phaseTableStyle">
		  <xsl:apply-templates select="chklstItem">
		  </xsl:apply-templates>
		</table>
    </div>
  </xsl:template>
  
    <xsl:template match="chklstItem">
		<tr>
			<td class="itemDescStyle"><xsl:value-of select="@desc"/></td><td class="itemValueStyle"><xsl:value-of select="@value"/></td>
		</tr>
	</xsl:template>
  
</xsl:stylesheet>