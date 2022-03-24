class CustomerAction < ApplicationRecord
  belongs_to :transition

  validates  :transition_id, uniqueness: true
end
