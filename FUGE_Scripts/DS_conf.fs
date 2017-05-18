/* FUGE-LC Reference script 

	Author: JPA
	Date  : 01-2010

 	Note: the name of the functions cannot be changed 							
*/

// Experiment name appearing in the logs
experimentName = "mlbd2017cnf";

// Directory where logs, fuzzy systems and temporary files are saved
savePath = "/home/mlbd/Desktop/mileRslt/";

// Fuzzy system parameters 
fixedVars = false;
nbRules = 5;		// Maximum number of rules
nbMaxVarPerRule = 5;	// Maximum number of vars per rule 
nbOutVars = 1;		// Number of output variables
nbInSets = 2;		// Number of input membership functions (MFs)
nbOutSets = 2;		// Number of output MFs
inVarsCodeSize = 5;	// Bits used to encode the index of an input variab (rule genome)
outVarsCodeSize = 1;	// Bits used to encode the index of an output variable index (rule genome)
inSetsCodeSize = 2;	// Bits used to encode the input MFs
outSetsCodeSize = 1;	// Bits used to encode the output MFs
inSetsPosCodeSize = 6;	// Bits used to encode the values of the input MFs
outSetPosCodeSize = 1;	// Bits used to encode the values of the output MFs

// Co-evolution parameters
// Population 1: Membership Functions (Variables)
maxGenPop1 = 200;
maxFitPop1 = 0.99;
elitePop1 = 5;
popSizePop1 = 200;
cxProbPop1 = 0.9;
mutFlipIndPop1 = 0.2;
mutFlipBitPop1 = 0.025;

// Population 2: Rules
elitePop2 = 5;
popSizePop2 = 200;
cxProbPop2 = 0.6;
mutFlipIndPop2 = 0.5;
mutFlipBitPop2 = 0.025;

// Fitness parameters
sensitivityW = 1.0;
specificityW = 1.0;
accuracyW = 0.0;
ppvW = 0.0;
rmseW = 0.0;
rrseW = 0.0;
raeW = 0.0;
mxeW = 0.0;
distanceThresholdW = 0.0;
distanceMinThresholdW = 0.0;
dontCareW = 0.0;
overLearnW = 0.0;
threshold = 0.5;
threshActivated = true;
sizeW = 0.122222;

function doSetParams()
{
	this.setParams(experimentName, savePath, fixedVars, nbRules, nbMaxVarPerRule, nbOutVars, nbInSets, nbOutSets, inVarsCodeSize, outVarsCodeSize, 
				 inSetsCodeSize, outSetsCodeSize, inSetsPosCodeSize, outSetPosCodeSize, maxGenPop1, maxFitPop1, elitePop1, popSizePop1, 
				 cxProbPop1, mutFlipIndPop1, mutFlipBitPop1, maxGenPop1, maxFitPop1, elitePop2, popSizePop2, cxProbPop2, mutFlipIndPop2, 
				 mutFlipBitPop2, sensitivityW, specificityW, accuracyW, ppvW, rmseW, rrseW, raeW, mxeW, distanceThresholdW,
			         distanceMinThresholdW, dontCareW, overLearnW, threshold, threshActivated, sizeW);
}

// Run function called by FUGE-LC. This function MUST also be present
function doRun() 
{
	var nbGens = [maxGenPop1];
	var pop1Vals = [popSizePop1];
	var pop2Vals = [popSizePop2]; //same size that pop1Vals		
	var nRuleVals = [nbRules];
	var nMaxVarPRule = [nbMaxVarPerRule];
	var alphaValues = [0.0, 0.2, 0.4, 0.6, 0.8, 1.0];
	var qtyExp = 10;

	// Multiple coevolution runs with different parameters
	for (var i = 0; i < nbGens.length; i++) {
		for(var j = 0; j < pop1Vals.length; j++) {
			for (var k = 0; k < nRuleVals.length; k++) {
				for (var l = 0; l < nMaxVarPRule.length; l++) {
					rmseW = 1.0;
					for (m = 0; m < alphaValues.length; m++)
					{
						sensitivityW = alphaValues[m];
						specificityW = alphaValues[m];
						for (var n = 0; n < qtyExp; n++) {
							maxGenPop1 = nbGens[i]
							popSizePop1 = pop1Vals[j];
							popSizePop2 = pop2Vals[j];
							nbRules = nRuleVals[k];
							nbMaxVarPerRule = nMaxVarPRule[l]			
							this.runEvo();
						}
					}
					sensitivityW = 1.0;
					specificityW = 1.0;
					for (m = (alphaValues.length-2); m >= 0; m--)
					{
						rmseW = alphaValues[m];
						for (var n = 0; n < qtyExp; n++) {
							maxGenPop1 = nbGens[i]
							popSizePop1 = pop1Vals[j];
							popSizePop2 = pop2Vals[j];
							nbRules = nRuleVals[k];
							nbMaxVarPerRule = nMaxVarPRule[l]			
							this.runEvo();
						}
					}
				}
			}
		}
	}
}
// EOF
