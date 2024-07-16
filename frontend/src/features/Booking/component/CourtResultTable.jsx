import CourtItem from "./CourtItem";


export function CourtResultTable({objectList}){
    return(
        <>
        <div className="object-table">
            {objectList.map((item) => (
				<CourtItem object={item} />
			))}
        </div>
      </>
    )
}
