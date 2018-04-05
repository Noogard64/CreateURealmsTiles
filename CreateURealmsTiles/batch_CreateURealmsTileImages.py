#!/usr/bin/env python

import os
from gimpfu import *

def run(inputFileNameAndPath):

	##########################################################
	#Part 0 - Setup
	##########################################################
	
	#Vars from Input File	
	#inputFileNameAndPath = 'C:\Users\sean-\Desktop\example.png'
	filePath = os.path.dirname(inputFileNameAndPath)
	fileName = os.path.basename(inputFileNameAndPath)
	fileNameNoExt = os.path.splitext(os.path.basename(fileName))[0]
	outputFolder = filePath + '\\' + fileNameNoExt 
	outputFileName = fileNameNoExt
	inputFile = inputFileNameAndPath
	
	#Create output folder
	if not os.path.exists(outputFolder):
		os.makedirs(outputFolder)
	
	#Get current directory to set asset paths
	currentDirectory = os.getcwd()
	
	#Set images assets path
	imageAssetPath = currentDirectory + '\image_Assets'
	
	imageAssetPath_Base = imageAssetPath+'\BlankTileTemplate.png'
	imageAssetPath_Blinded = imageAssetPath +'\\blinded.png'
	imageAssetPath_Burning = imageAssetPath +'\\burning.png'
	imageAssetPath_Charmed = imageAssetPath +'\\charmed.png'
	imageAssetPath_Defeated = imageAssetPath +'\\defeated.png'
	imageAssetPath_Frozen = imageAssetPath +'\\frozen.png'
	imageAssetPath_Poisoned = imageAssetPath +'\\poisoned.png'
	imageAssetPath_Silenced = imageAssetPath +'\\silenced.png'
	imageAssetPath_Stunned = imageAssetPath +'\\stunned.png'

	##########################################################
	#Part 1 - Get the circle image
	##########################################################

	try:
		#Load File
		pdb.gimp_message('Loading file...')
		image_Input = pdb.file_png_load(inputFile, inputFile)

		#Scale file to correct size
		pdb.gimp_message('Scaling image...')
		pdb.gimp_image_scale(image_Input, 284, 284)
		
		#Get ellipse selection (circle)
		pdb.gimp_message('Ellipsing image...')
		pdb.gimp_image_select_ellipse(image_Input, 2, 0, 0, 286, 286)

		#Copy circle
		pdb.gimp_message('Copying image...')	
		pdb.gimp_edit_copy(image_Input.layers[0])

		#Paste Circle to new image_Input
		pdb.gimp_message('Pasting image...')		
		newImage = pdb.gimp_edit_paste_as_new()
		
		#Flip Image
		pdb.gimp_message('Flipping image...')	
		pdb.gimp_image_flip(newImage, 1)

		#Resize image_Input
		pdb.gimp_message('Resizing image...')	
		pdb.gimp_layer_resize(newImage.layers[0], 512, 512, 0, 0)

		#Save circle as new image_Input
		pdb.gimp_message('Saving file...')	
		outputFile = filePath + '\saved_imageAsCircle.png'
		pdb.file_png_save_defaults(newImage, newImage.active_layer, outputFile, outputFile)
	except Exception as e:
		print e.args[0]
		pdb.gimp_message(e.args[0])
	##########################################################
	#Part 2 - Put Circle image on template
	##########################################################
	try:
		#Create new image
		pdb.gimp_message('Creating new image...')	
		image_New = gimp.Image(512, 512)
			
		#Adds tile template to image
		pdb.gimp_message('Merging images...')	
		File_template = imageAssetPath_Base
		layer_Template = pdb.gimp_file_load_layer(image_New, File_template)
		pdb.gimp_image_insert_layer(image_New, layer_Template, None, 0)

		#Adds circle image to image
		pdb.gimp_message('Merging another image...')	
		layer_Circle = pdb.gimp_file_load_layer(image_New, outputFile)
		pdb.gimp_image_insert_layer(image_New, layer_Circle, None, 0)
		layer = pdb.gimp_image_merge_down(image_New, layer_Circle, 1)
		
		#Saves base new image
		pdb.gimp_message('Saving another file...')		
		File_Base = outputFolder + '\saved_BaseTile.png'
		pdb.file_png_save_defaults(image_New, image_New.active_layer, File_Base, File_Base)
	except Exception as e:
		print e.args[0]
		pdb.gimp_message(e.args[0])
	##########################################################
	#Part 3 - Create Status Effect images
	##########################################################

	# statusEffects = [imageAssetPath_Blinded,imageAssetPath_Burning,imageAssetPath_Charmed,imageAssetPath_Defeated,imageAssetPath_Frozen,imageAssetPath_Poisoned,imageAssetPath_Silenced,imageAssetPath_Stunned]
		
	# for status in statusEffects:
		# pdb.gimp_message(x)
		# CreateStatusEffectImage(status,File_Base,outputFolder,outputFileName)
		
		
	status = 'Blind'
	CreateStatusEffectImage(status,imageAssetPath_Blinded,File_Base,outputFolder,outputFileName)
	
	status = 'Burning'
	CreateStatusEffectImage(status,imageAssetPath_Burning,File_Base,outputFolder,outputFileName)
		
	status = 'Charmed'
	CreateStatusEffectImage(status,imageAssetPath_Charmed,File_Base,outputFolder,outputFileName)
	
	status = 'Defeated'
	CreateStatusEffectImage(status,imageAssetPath_Defeated,File_Base,outputFolder,outputFileName)

	status = 'Frozen'
	CreateStatusEffectImage(status,imageAssetPath_Frozen,File_Base,outputFolder,outputFileName)

	status = 'Poisoned'
	CreateStatusEffectImage(status,imageAssetPath_Poisoned,File_Base,outputFolder,outputFileName)
	
	status = 'Silenced'
	CreateStatusEffectImage(status,imageAssetPath_Silenced,File_Base,outputFolder,outputFileName)
	
	status = 'Stunned'
	CreateStatusEffectImage(status,imageAssetPath_Stunned,File_Base,outputFolder,outputFileName)
	
	##########################################################
	#Clean up
	##########################################################	
	os.remove(outputFile) 
	
def CreateStatusEffectImage(status,filePath,File_Base,outputFolder,outputFileName):
	try:
		#Create new image
		pdb.gimp_message('Creating status image [' + status + ']...')	
		image_New = gimp.Image(512, 512)

		#Adds tile template to image
		layer_Base = pdb.gimp_file_load_layer(image_New, File_Base)
		pdb.gimp_image_insert_layer(image_New, layer_Base, None, 0)

		#Adds circle image to image
		layer_Circle = pdb.gimp_file_load_layer(image_New, filePath)
		pdb.gimp_image_insert_layer(image_New, layer_Circle, None, 0)
		layer = pdb.gimp_image_merge_down(image_New, layer_Circle, 1)

		#Saves base new image
		File_Base = outputFolder + "\\" + outputFileName + "_" + status + ".png"
		pdb.file_png_save_defaults(image_New, image_New.active_layer, File_Base, File_Base)
	except Exception as e:
		print e.args[0]
		pdb.gimp_message(e.args[0])	
#def CreateImage():
#	return 




	


