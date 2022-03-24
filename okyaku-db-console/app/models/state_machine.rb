class StateMachine < ApplicationRecord
    has_many :states
    has_many :transitions
    belongs_to :scenario_category

    validates :scenario_category_id, presence: true
end
