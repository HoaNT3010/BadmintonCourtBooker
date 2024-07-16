import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

export default function Detail() {
    const objName = useParams();
    const flowers = useSelector((state) => state.flowers.flowers);
    
    const flowerlist = flowers.find((obj) => {
      return obj.id == objName.id;
    });
  
    return (
      <div className="card mb-3 flower-fullscreen">
        <div className="row g-0">
          <div className="col-md-4">
            <img className="card-img-top" src={flowerlist.picture} />
          </div>
          <div className="col-md-8">
            <div className="card-body">
              <h5 className="card-title">{flowerlist.name}</h5>
              <p className="card-text">
                Origin: {flowerlist.origin} <br />
                Description: {flowerlist.description} <br />
                Price: ${flowerlist.price}
              </p>
              <a href="/">Return</a>
            </div>
          </div>
        </div>
      </div>
    );
  }
  