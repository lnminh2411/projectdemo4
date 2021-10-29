jQuery(document).ready(function () {
	// click on next button
	jQuery('.form-wizard-next-btn').click(function () {
		var parentFieldset = jQuery(this).parents('.wizard-fieldset');
		var currentActiveStep = jQuery(this).parents('.form-wizard').find('.form-wizard-steps .active');
		var next = jQuery(this);
		var nextWizardStep = true;
		parentFieldset.find('.wizard-required').each(function () {
			var thisValue = jQuery(this).val();

			if (thisValue == "") {
				jQuery(this).siblings(".wizard-form-error").slideDown();
				nextWizardStep = false;
			}
			else {
				jQuery(this).siblings(".wizard-form-error").slideUp();
			}
		});
		if (nextWizardStep) {
			next.parents('.wizard-fieldset').removeClass("show", "400");
			currentActiveStep.removeClass('active').addClass('activated').next().addClass('active', "400");
			next.parents('.wizard-fieldset').next('.wizard-fieldset').addClass("show", "400");
			jQuery(document).find('.wizard-fieldset').each(function () {
				if (jQuery(this).hasClass('show')) {
					var formAtrr = jQuery(this).attr('data-tab-content');
					jQuery(document).find('.form-wizard-steps .form-wizard-step-item').each(function () {
						if (jQuery(this).attr('data-attr') == formAtrr) {
							jQuery(this).addClass('active');
							var innerWidth = jQuery(this).innerWidth();
							var position = jQuery(this).position();
							jQuery(document).find('.form-wizard-step-move').css({ "left": position.left, "width": innerWidth });
						} else {
							jQuery(this).removeClass('active');
						}
					});
				}
			});
		}
	});
	//click on previous button
	jQuery('.form-wizard-previous-btn').click(function () {
		var counter = parseInt(jQuery(".wizard-counter").text());;
		var prev = jQuery(this);
		var currentActiveStep = jQuery(this).parents('.form-wizard').find('.form-wizard-steps .active');
		prev.parents('.wizard-fieldset').removeClass("show", "400");
		prev.parents('.wizard-fieldset').prev('.wizard-fieldset').addClass("show", "400");
		currentActiveStep.removeClass('active').prev().removeClass('activated').addClass('active', "400");
		jQuery(document).find('.wizard-fieldset').each(function () {
			if (jQuery(this).hasClass('show')) {
				var formAtrr = jQuery(this).attr('data-tab-content');
				jQuery(document).find('.form-wizard-steps .form-wizard-step-item').each(function () {
					if (jQuery(this).attr('data-attr') == formAtrr) {
						jQuery(this).addClass('active');
						var innerWidth = jQuery(this).innerWidth();
						var position = jQuery(this).position();
						jQuery(document).find('.form-wizard-step-move').css({ "left": position.left, "width": innerWidth });
					} else {
						jQuery(this).removeClass('active');
					}
				});
			}
		});
	});
	//click on form submit button
	jQuery(document).on("click", ".form-wizard .form-wizard-submit", function () {
		var parentFieldset = jQuery(this).parents('.wizard-fieldset');
		var currentActiveStep = jQuery(this).parents('.form-wizard').find('.form-wizard-steps .active');
		parentFieldset.find('.wizard-required').each(function () {
			var thisValue = jQuery(this).val();
			if (thisValue == "") {
				jQuery(this).siblings(".wizard-form-error").slideDown();
			}
			else {
				jQuery(this).siblings(".wizard-form-error").slideUp();
			}
		});
	});
	// focus on input field check empty or not
	jQuery(".form-control").on('focus', function () {
		var tmpThis = jQuery(this).val();
		if (tmpThis == '') {
			jQuery(this).parent().addClass("focus-input");
		}
		else if (tmpThis != '') {
			jQuery(this).parent().addClass("focus-input");
		}
	}).on('blur', function () {
		var tmpThis = jQuery(this).val();
		if (tmpThis == '') {
			jQuery(this).parent().removeClass("focus-input");
			jQuery(this).siblings('.wizard-form-error').slideDown("3000");
		}
		else if (tmpThis != '') {
			jQuery(this).parent().addClass("focus-input");
			jQuery(this).siblings('.wizard-form-error').slideUp("3000");
		}
	});
});






////////////////////////

$(document).ready(function () {
	if (window.File && window.FileList && window.FileReader) {
		$("#files").on("change", function (e) {
			var files = e.target.files,
				filesLength = files.length;
			for (var i = 0; i < filesLength; i++) {
				var f = files[i]
				var fileReader = new FileReader();
				fileReader.onload = (function (e) {
					var file = e.target;
					$("<span class=\"pip\">" +
						"<img class=\"imageThumb\" src=\"" + e.target.result + "\" title=\"" + file.name + "\"/>" +
						"<br/><span class=\"remove\">Remove image</span>" +
						"</span>").insertAfter("#files");
					$(".remove").click(function () {
						$(this).parent(".pip").remove();
					});

					// Old code here
					/*$("<img></img>", {
					  class: "imageThumb",
					  src: e.target.result,
					  title: file.name + " | Click to remove"
					}).insertAfter("#files").click(function(){$(this).remove();});*/

				});
				fileReader.readAsDataURL(f);
			}
			console.log(files);
		});
	} else {
		alert("Your browser doesn't support to File API")
	}
});



//$(function () {
//	$("#sortableImgThumbnailPreview").sortable({
//		connectWith: ".RearangeBox",


//		start: function (event, ui) {
//			$(ui.item).addClass("dragElemThumbnail");
//			ui.placeholder.height(ui.item.height());

//		},
//		stop: function (event, ui) {
//			$(ui.item).removeClass("dragElemThumbnail");
//		}
//	});
//	$("#sortableImgThumbnailPreview").disableSelection();
//});




//document.getElementById('files').addEventListener('change', handleFileSelect, false);

//function handleFileSelect(evt) {

//	var files = evt.target.files;
//	var output = document.getElementById("sortableImgThumbnailPreview");

//	// Loop through the FileList and render image files as thumbnails.
//	for (var i = 0, f; f = files[i]; i++) {

//		// Only process image files.
//		if (!f.type.match('image.*')) {
//			continue;
//		}

//		var reader = new FileReader();

//		// Closure to capture the file information.
//		reader.onload = (function (theFile) {
//			return function (e) {
//				// Render thumbnail.
//				var imgThumbnailElem = "<div class='RearangeBox imgThumbContainer'><i class='material-icons imgRemoveBtn' onclick='removeThumbnailIMG(this)'>cancel</i><div class='IMGthumbnail' ><img  src='" + e.target.result + "'" + "title='" + theFile.name + "'/></div><div class='imgName'>" + theFile.name + "</div></div>";

//				output.innerHTML = output.innerHTML + imgThumbnailElem;

//			};
//		})(f);

//		// Read in the image file as a data URL.
//		reader.readAsDataURL(f);
//	}
//}

//function removeThumbnailIMG(elm) {
//	elm.parentNode.outerHTML = '';
//}

