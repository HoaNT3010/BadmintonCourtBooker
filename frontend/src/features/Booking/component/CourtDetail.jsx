import { Button } from "react-bootstrap";
import { useSelector } from "react-redux";
import { Link, useParams } from "react-router-dom";

export default function CourtDetail() {
    const objName = useParams();
    const flowers = useSelector((state) => state.flowers.flowers);
    
    const flowerlist = flowers.find((obj) => {
      return obj.id == objName.id;
    });
  
    return (
      <div className="card mb-3">
        <div className="row g-0">
          <div className="col-md-4 card-img-holder">
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
              <Link to={`/`} >
                <Button variant="secondary">Return</Button>
              </Link>
            </div>
          </div>
        </div>
      </div>
    );
  }
  