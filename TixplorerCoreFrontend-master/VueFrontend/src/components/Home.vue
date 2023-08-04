<script>
// import WelcomeItem from './WelcomeItem.vue'
</script>

<template>
  <!-- <div id="list-example" class="list-group listleft d-none d-xl-block">
      <a class="list-group-item list-group-item-action" href="#list-item-1"
        >最頂端</a
      >
      <a class="list-group-item list-group-item-action" href="#list-item-2"
        >超值優惠</a
      >
      <a class="list-group-item list-group-item-action" href="#list-item-3"
        >限時優惠</a
      >
      <a class="list-group-item list-group-item-action" href="#list-item-4"
        >認識我們</a
      >
      <a class="list-group-item list-group-item-action" href="#list-item-5"
        >合作夥伴</a
      >
    </div> -->
  <main>
    <div
      data-bs-spy="scroll"
      data-bs-target="#list-example"
      data-bs-offset="0"
      class="scrollspy-example"
      tabindex="0"
    >
      <div class="col mb-5 mx-0 mt-0">
        <!--HomePic圖片來源自10wallpaper.com-->
        <div
          id="carouselExampleAutoplaying"
          class="carousel slide tidy HomeSearch"
          data-bs-ride="carousel"
        >
          <div class="top-layer d-xl-block">
            <h1 class="text-light">擇你所愛</h1>
            <h3 class="text-light">尋覓台灣的各個美好角落 立即尋找</h3>
            <div class="d-flex" role="search">
              <input
                class="form-control me-2 searchTextType"
                id="Searchtxt"
                type="search"
                placeholder="快點搜尋想去的地方！"
                aria-label="Search"
              />
              <input
                type="button"
                class="btn btn-outline-success bg-info text-light searchTextType"
                value="搜尋"
                onclick="doSearch()"
              />
            </div>
          </div>
          <div class="carousel-inner">
            <div class="carousel-item active">
              <img
                src="./../assets/images/HomePic1.png"
                class="d-block w-100 HomePic"
                alt=""
                style="margin: 0px"
              />
            </div>
            <div class="carousel-item">
              <img
                src="./../assets/images/HomePic2.png"
                class="d-block w-100 HomePic"
                alt=""
                style="margin: 0px"
              />
            </div>
            <div class="carousel-item">
              <img
                src="./../assets/images/HomePic3.png"
                class="d-block w-100 HomePic"
                alt=""
                style="margin: 0px"
              />
            </div>
            <div class="carousel-item">
              <img
                src="./../assets/images/HomePic4.png"
                class="d-block w-100 HomePic"
                alt=""
                style="margin: 0px"
              />
            </div>
          </div>
          <button
            class="carousel-control-prev"
            type="button"
            data-bs-target="#carouselExampleAutoplaying"
            data-bs-slide="prev"
          >
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
          </button>
          <button
            class="carousel-control-next"
            type="button"
            data-bs-target="#carouselExampleAutoplaying"
            data-bs-slide="next"
          >
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
          </button>
        </div>
      </div>
      <div class="container">
        <div class="row mx-auto mb-5">
          <div class="col SearchLogo shadow mx-2">
            <a type="button" href="/Search?productType=1&productCondition=0">
              <img class="SearchLogoImg" src="./../assets/images/ActiveLogo.png" />
            </a>
          </div>
          <div class="col SearchLogo shadow mx-2">
            <a type="button" href="/Search?productType=1&productCondition=1">
              <img class="SearchLogoImg" src="./../assets/images/AttractionLogo.png" />
            </a>
          </div>
          <div class="col SearchLogo shadow mx-2">
            <a type="button" href="/Search?productType=1&productCondition=2">
              <img class="SearchLogoImg" src="./../assets/images/HotelLogo.png" />
            </a>
          </div>
          <div class="col SearchLogo shadow mx-2">
            <a type="button" href="/Search?productType=1&productCondition=3">
              <img class="SearchLogoImg" src="./../assets/images/RestaurantLogo.png" />
            </a>
          </div>
        </div>
        <!-- <h1 id="list-item-2">超值優惠</h1>
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
          <div class="carousel-inner mb-5">
            <div class="carousel-item active">
              <div class="row justify-content-around">
                <div class="col-3">
                  <a href="#">
                    <img src="images/活動優惠1.png" alt="Image 1" />
                  </a>
                </div>
                <div class="col-3">
                  <a href="#">
                    <img src="images/活動優惠2.png" alt="Image 2" />
                  </a>
                </div>
                <div class="col-3">
                  <a href="#">
                    <img src="images/活動優惠3.png" alt="Image 3" />
                  </a>
                </div>
              </div>
            </div>
            <div class="carousel-item">
              <div class="row justify-content-around">
                <div class="col-3">
                  <a href="#">
                    <img src="images/活動優惠4.png" alt="Image 4" />
                  </a>
                </div>
                <div class="col-3">
                  <a href="#">
                    <img src="images/活動優惠5.png" alt="Image 5" />
                  </a>
                </div>
                <div class="col-3">
                  <a href="#">
                    <img src="images/活動優惠6.png" alt="Image 6" />
                  </a>
                </div>
              </div>
            </div>
          </div>
          <a
            class="carousel-control-prev"
            href="#myCarousel"
            role="button"
            data-slide="prev"
          >
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a
            class="carousel-control-next"
            href="#myCarousel"
            role="button"
            data-slide="next"
          >
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>
        </div> -->
      </div>
    </div>
  </main>
</template>

<script setup>
// import WelcomeItem from './WelcomeItem.vue'
</script>
