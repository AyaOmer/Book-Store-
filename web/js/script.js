searchForm=document.querySelector('.search-form');
document.querySelector('#search-icon').onclick=() =>{
  searchForm.classList.toggle('active');  
};
window.onscroll=()=>{
    if(window.screenY>80){
        searchForm.classList.remove('active');  
        document.querySelector('.header .header-2').classList.add('active');

    }
    else{
        document.querySelector('.header .header-2').classList.remove('active');
    }
}

window.onload=()=>{
    if(window.screenY>80){
        document.querySelector('.header .header-2').classList.add('active');

    }
    else{
        document.querySelector('.header .header-2').classList.remove('active');
    }
}