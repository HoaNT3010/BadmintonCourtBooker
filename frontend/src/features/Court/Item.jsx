import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";

export default function Item({object}){
    return (
        <Card className="item-card">
        <Card.Img variant="top" src={object.picture} ></Card.Img>
        <Card.Body>
            <Card.Title>{object.name}</Card.Title>
        </Card.Body>
        <Link to={`detail/${object.id}`}>
                  <Button>Detail</Button>
        </Link>
      </Card>
    )
}
