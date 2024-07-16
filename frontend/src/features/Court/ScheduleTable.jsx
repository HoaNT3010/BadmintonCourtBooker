export default function ScheduleTable() {
  return (
    <div>
      <div className="schedula-day-bar"></div>
      <Table striped className="table-admin">
        <thead>
          <tr>
            <th>Monday</th>
          </tr>
        </thead>
        <tbody>
          {matchItems.map((item) => (
            <tr>
              <td>{item.id}</td>
              <td>
                <img className="table-img" src={item.picture} />
              </td>
              <td>{item.name}</td>
              <td>{item.description}</td>
              <td>{item.origin}</td>
              <td>${item.price}</td>
              <td>{item.stockCount}</td>
              <td>
                <div className="admin-table-button d-flex flex-row">
                  <Link to={`/admin/udpate/${item.id}`}>
                    <Button variant="success">Edit</Button>
                  </Link>
                  &nbsp;&nbsp;
                  <Button
                    variant="danger"
                    onClick={() => handleDelete(item.id)}
                  >
                    Delete
                  </Button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
}
