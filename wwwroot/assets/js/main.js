
// function responsiveSlider() {
//     const slider = document.querySelector('.container');
//     let sliderWidth = slider.offsetWidth / 3;
//     const sliderList = document.querySelector('ul');
//     let items = sliderList.querySelectorAll('img').length -1 ;
//     let count = 1;
    
//     window.addEventListener('resize', function() {
//       sliderWidth = slider.offsetWidth;
//     });
    
//     function prevSlide() {
//       if(count > 1) {
//         count = count - 2;
//         sliderList.style.left = '-' + count * sliderWidth + 'px';
//         count++;
//       }else if(count == 1) {
//         count = items - 1;
//         sliderList.style.left = '-' + count * sliderWidth + 'px';
//         count++;
//       }
      
//     }
//     function nextSlide() {
//       if(count < items) {
//         sliderList.style.left = '-' + count * sliderWidth + 'px';
//         count++;
        
//       }else if(count == items) {
//         sliderList.style.left = '0px';
//         count = 1;
        
//       }
//     }
//     prev.addEventListener('click', prevSlide);
//     next.addEventListener('click', nextSlide);
//     setInterval(function() {
//       nextSlide();
//     }, 4000);
//   }
  
//   window.onload = function() {
//     responsiveSlider();
//   }
  
  
var lowerSlider = document.querySelector('#lower');
var  upperSlider = document.querySelector('#upper');

document.querySelector('#two').value=upperSlider.value;
document.querySelector('#one').value=lowerSlider.value;

var  lowerVal = parseInt(lowerSlider.value);
var upperVal = parseInt(upperSlider.value);

upperSlider.oninput = function () {
    lowerVal = parseInt(lowerSlider.value);
    upperVal = parseInt(upperSlider.value);

    if (upperVal < lowerVal + 4) {
        lowerSlider.value = upperVal - 4;
        if (lowerVal == lowerSlider.min) {
        upperSlider.value = 4;
        }
    }
    document.querySelector('#two').value=this.value
};

lowerSlider.oninput = function () {
    lowerVal = parseInt(lowerSlider.value);
    upperVal = parseInt(upperSlider.value);
    if (lowerVal > upperVal - 4) {
        upperSlider.value = lowerVal + 4;
        if (upperVal == upperSlider.max) {
            lowerSlider.value = parseInt(upperSlider.max) - 4;
        }
    }
    document.querySelector('#one').value=this.value
};

//quantity
function increaseCount(e, el) {
    var input = el.previousElementSibling;
    var value = parseInt(input.value, 10);
    if(value < 10){

        alue = isNaN(value) ? 0 : value;
        value++;
        input.value = value;
    }
  }
  function decreaseCount(e, el) {
    var input = el.nextElementSibling;
    var value = parseInt(input.value, 10);
    if (value > 1) {
      value = isNaN(value) ? 0 : value;
      value--;
      input.value = value;
    }
  }

(function () {
    document.getElementById("cart").on("click", function () {
        document.getElementByClassName("shopping-cart").fadeToggle("fast");
    });
})();

  
  

